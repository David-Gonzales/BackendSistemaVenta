using Domain.Common;

namespace Domain.Entities
{
    public class Cliente : AuditableBaseEntity
    {
        private int _edad;
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string TipoDocumento { get; set; }
        public required string NumeroDocumento { get; set; }
        public required string Correo { get; set; }
        public required string Ciudad { get; set; }
        public required string Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsActivo { get; set; }
        public int Edad
        {
            get
            {
                if (this._edad <= 0)
                {
                    this._edad = new DateTime(DateTime.Now.Subtract(this.FechaNacimiento).Ticks).Year - 1;
                }
                return this._edad;
            }
            set
            {
                this._edad = value;
            }
        }

        //Relación - Ventas realizadas hacia este cliente
        public ICollection<Venta>? Ventas { get; set; }
    }
}
