﻿using QTTaxiDriver.Logic.Models.App;

namespace QTTaxiDriver.Logic.Controllers.App
{
    partial class DrivesController
    {
        internal override IEnumerable<string> Includes => new string[] { nameof(Entities.App.Drive.Vehicle), nameof(Entities.App.Drive.Driver) };

        public override Drive Create()
        {
            var result = base.Create();

            result.Date = DateTime.UtcNow;
            return result;
        }
        protected override void ValidateEntity(ActionType actionType, Entities.App.Drive entity)
        {
            if (entity.Date > DateTime.UtcNow)
            {
                throw new Modules.Exceptions.LogicException("Invalid date value!");
            }
            if (entity.Distance < 0 || entity.Distance > 1_000)
            {
                throw new Modules.Exceptions.LogicException("Invalid distance value");
            }
            base.ValidateEntity(actionType, entity);
        }
    }
}
