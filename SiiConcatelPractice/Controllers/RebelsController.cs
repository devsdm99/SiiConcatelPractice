using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using SiiConcatelPractice.Excepcions;
using SiiConcatelPractice.Models;
using SiiConcatelPractice.Repositorys;
using SiiConcatelPractice.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelsController : ControllerBase
    {
        private readonly IRebelRepository _rebelRepository;
        private readonly ILogger<RebelsController> _logger;

        public RebelsController(IRebelRepository rebelRepository, ILogger<RebelsController> logger)
        {
            _rebelRepository = rebelRepository;
            _logger = logger;
        }
        /// <summary>
        /// Gets All the Rebels from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Rebel>> GetAllRebels()
        {
            _logger.LogInformation("Start of RebelsController.GetAllRebels()");
            ActionResult response = null;

            try
            {
                response = Ok(new ApiResult()
                {
                    Data = _rebelRepository.GetAllRebels(),
                    Message = "List of rebels"
                });
                _logger.LogInformation("Rebels received");


                _logger.LogInformation("End of RebelsController.GetAllRebels()");

            }
            catch (Exception ex)
            {
                _logger.LogError($"There was a problem trying to get all rebels, error: " + ex.Message);
                response = BadRequest(new ApiResult()
                {
                    Message = "There was a problem trying to get all rebels",
                    IsError = true
                });
            }

            return response;
        }

        /// <summary>
        /// Gets a Rebel by ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Rebel> GetRebelById(int id)
        {
            var result = new ApiResult();
            ActionResult response = null;

            _logger.LogInformation("Start of RebelsController.GetRebelById()");

            try
            {
                _logger.LogDebug($"Trying to get rebel id: {id}");
                if (id > 0)
                {
                    var rebel = _rebelRepository.GetRebelById(id);

                    if (rebel == null)
                    {
                        _logger.LogWarning($"The rebel with id {id} has not been found");
                        result.Message = $"The rebel was not found";
                        result.IsError = true;
                        response = BadRequest(result);
                        throw new InvalidValueException("The rebel was not found");

                    }
                    else
                    {
                        _logger.LogDebug($"Rebel: {id} found");

                        result.Data = rebel;
                        result.Message = $"Rebel: {rebel.Id} found";
                        response = Ok(rebel);
                    }
                }
            }
            catch (InvalidValueException ex)
            {
                _logger.LogError("There was an error executing RebelsController.GetRebelByid() error: " + ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Creates a new Rebel or List of Rebels
        /// </summary>
        /// <param name="rebels"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResult> Post([FromBody] RebelList rebels)
        {
            _logger.LogInformation("Start of RebelsController.Post()");
            var result = new ApiResult();
            ActionResult response = null;

            try
            {

                if (!ModelState.IsValid)
                {
                    result.Message = "Invalid rebel information.";
                    result.IsError = true;
                    _logger.LogWarning("There was a problem creating a new Rebel, required data");
                    response = BadRequest(result);

                }
                else
                {
                    rebels.Rebels.ForEach(x => x.Date = DateTime.Now);
                    _rebelRepository.AddRebels(rebels.Rebels);

                    result.Message = "Rebels added succesfullly.";
                    result.Data = rebels.Rebels;
                    response = Ok(result);
                    _logger.LogDebug("Rebels inserted");


                }
            }
            catch (SqliteException ex)
            {
                _logger.LogError("An error ocurred while Post method was running, error:", ex.Message);
                response = BadRequest(result);

            }
            _logger.LogInformation("End of RebelsController.Post()");

            return response;
        }

        /// <summary>
        /// Modifies an existing Rebel
        /// </summary>
        /// <param name="rebel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApiResult> Put([FromBody] Rebel rebel)
        {
            _logger.LogInformation("Start of RebelsController.Put()");
            var result = new ApiResult();
            ActionResult response = null;

            try
            {

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("There was a problem updating rebel, invalid data");
                    result.Message = "Invalid rebel information.";
                    result.IsError = true;

                    response = BadRequest(result);
                }
                else
                {
                    _logger.LogDebug($"Trying to get rebel id: {rebel.Id}");
                    var filteredRebel = _rebelRepository.GetRebelById(rebel.Id);

                    if (filteredRebel == null)
                    {

                        _logger.LogWarning($"The rebel with id {rebel.Id} has not been found");
                        result.Message = $"The rebel with id {rebel.Id} has not been found";
                        result.IsError = true;
                        response = BadRequest(result);

                        throw new InvalidValueException("The rebel was not found");

                    }
                    else
                    {
                        filteredRebel.Name = rebel.Name;
                        filteredRebel.Planet = rebel.Planet;
                        filteredRebel.Date = DateTime.Now;

                        _rebelRepository.UpdateRebel(filteredRebel);
                        result.Message = "Rebel updated correctly.";
                        result.Data = filteredRebel;

                        _logger.LogDebug($"Rebel {filteredRebel.Id} updated");
                        response = Ok(result);
                    }
                }


            }
            catch (InvalidValueException ex)
            {
                _logger.LogError("An error ocurred while updating " + ex.Message);
            }
            _logger.LogInformation("End of RebelsController.Put()");

            return response;
        }

        /// <summary>
        /// Removes a Rebel from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ApiResult> Delete(int id)
        {
            _logger.LogInformation("Start of RebelsController.Delete()");
            var result = new ApiResult();
            ActionResult response = null;
            _logger.LogDebug($"Trying to delete rebel ID: {id}");

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("There was a problem deleting rebel, invalid data");
                    result.Message = "Invalid rebel information.";
                    result.IsError = true;
                    response = BadRequest(result);
                }
                else
                {
                    if (id > 0)
                    {
                        var filteredRebel = _rebelRepository.GetRebelById(id);

                        if (filteredRebel == null)
                        {
                            _logger.LogWarning($"The rebel {id} was not found");
                            result.Message = $"The rebel with id {id} has not been found";
                            result.IsError = true;

                            response = BadRequest(result);
                            throw new InvalidValueException("The rebel was not found");

                        }
                        else
                        {
                            _rebelRepository.DeleteRebel(filteredRebel);
                            result.Message = "Rebel deleted correctly";
                            result.Data = filteredRebel;
                            _logger.LogDebug($"Rebel {id} sucessfully deleted");

                            response = Ok(result);
                        }
                    }
                }

            }
            catch (InvalidValueException ex)
            {
                _logger.LogError("An error occurred while Deleted method was running, error :" + ex.Message);
            }

            _logger.LogInformation("End of RebelsController.Delete()");
            return response;
        }


    }
}
