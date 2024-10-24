using Microsoft.AspNetCore.Mvc;
using Observer.Constants;
using Observer.Domain.Interfaces;
using Observer.Presentation.Errors;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;
using SingleLog.Enums;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;

namespace Observer.Controllers
{
    /// <summary>
    /// Product Controller.
    /// </summary>
    [ApiController]
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ISingleLog<LogModel> _singleLog;

        /// <summary>
        /// General constructor.
        /// </summary>
        /// <param name="productServices">Service class based on IProductServices.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public ProductController(IProductServices productServices, ISingleLog<LogModel> singleLog)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));
        }

        /// <summary>
        /// GET endpoint to retrieve some product data.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseEnvelope>> ProductDetails(int productId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.GET_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var response = await _productServices.RetrieveProduct(productId);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// POST endpoint to create a new user in the sistem.
        /// </summary>
        /// <param name="product">Object ProductRequest with user data.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductCreate(ProductRequest product)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();

            baseLog.Request = product;

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.CREATE_NEW_USER, sublog);

            sublog.StopwatchStart();

            try
            {
                var validation = product.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _productServices.CreateProduct(product);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.Created) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// PUT endpoint to edit data for a specific user.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <param name="user">Object ProductRequest with user data.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpPut]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductEdit(int productId, ProductRequest user)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId, user };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.EDIT_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _productServices.UpdateProduct(productId, user);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return response;
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// DELETE endpoint to delete some user into sistem.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductDelete(int productId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.DELETE_USER_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var response = await _productServices.DeleteProduct(productId);

                baseLog.Response = response;
                baseLog.Level = response.ResponseCode.Equals(HttpStatusCode.OK) ? LogTypes.INFO : LogTypes.WARN;

                return Ok(response);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }
    }
}
