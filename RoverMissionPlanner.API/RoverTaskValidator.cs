using FluentValidation;
using RoverMissionPlanner.Domain;

namespace RoverMissionPlanner.API
{
    public class RoverTaskValidator : AbstractValidator<RoverTask>
    {
        public RoverTaskValidator()
        {
            RuleFor(x => x.RoverName)
                .NotEmpty().WithMessage("El nombre del rover es obligatorio.");

            RuleFor(x => x.TaskType)
                .IsInEnum().WithMessage("El tipo de tarea no es válido.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("La latitud debe estar entre -90 y 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("La longitud debe estar entre -180 y 180.");

            RuleFor(x => x.StartsAt)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.");

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("La duración debe ser mayor a 0 minutos.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("El estado de la tarea no es válido.");
        }
    }
} 