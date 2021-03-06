﻿using Hangfire;
using Microsoft.AspNetCore.Mvc;
using POC.Domain.Application;
using POC.Domain.Contracts;

namespace POC.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        #region Properties

        /// <summary>Feature de application</summary>
        private IFeatureApplication FeatureApplication { get; }

        /// <summary>Background hangout client</summary>
        private IBackgroundJobClient BackgroundJobClient { get; }

        #endregion

        /// <summary>Construtor do hangfire controller</summary>
        /// <param name="featureApplication">Feature de application</param>
        /// <param name="backgroundJobClient">Background hangout client</param>
        public HangFireController(
                                    IFeatureApplication featureApplication,
                                    IBackgroundJobClient backgroundJobClient
                                 )
        {
            FeatureApplication = featureApplication;
            BackgroundJobClient = backgroundJobClient;
        }

        /// <summary>Obtém um objeto do front e enfilera para ser processado no serverworker</summary>
        /// <param name="value">Dados de contrato recebido pelo front</param>
        [HttpPut]
        public IActionResult Put([FromBody] DataContractSample value)
        {
            BackgroundJobClient
                .Enqueue<IFeatureApplication>(c => c.Execute(value));

            return Accepted();
        }
    }
}
