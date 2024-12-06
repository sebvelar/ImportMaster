using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "El nombre debe contener al menos 3 caracteres")]
        [MaxLength(100, ErrorMessage = "El nombre no puede contener más de 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Porfavor ingrese un valor mayor a 0")]
        public int Existencias { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Porfavor ingrese un valor mayor a 0")]
        public float Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Porfavor ingrese un valor mayor a 0")]
        public int PorcentajeImpuesto { get; set; }

        public int OrdenCompraId { get; set; }

        public ICollection<OrdenCompraProducto> OrdenCompraProductos { get; set; }


    }
}
