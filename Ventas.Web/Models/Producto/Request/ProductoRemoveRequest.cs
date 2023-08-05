﻿namespace Ventas.Web.Models.Producto.Request
{
    public class ProductoRemoveRequest : BaseRequest
    {
        public int IdProducto { get; set; }
       

        public ProductoRemoveRequest()
        {

        }

        public ProductoRemoveRequest(int idProducto)
        {
            this.IdProducto = idProducto;
        }
    }


}
