using Microsoft.AspNetCore.Mvc;
using StockControlAPI.Domain.Model;
using StockControlAPI.Domain.Responses;

namespace StockControlAPI.Hateoas
{
    public static class ProductHateoasBuilder
    {
        public static ApiResponse<Product> Build(Product product, IUrlHelper urlHelper)
        {
            var response = ApiResponse<Product>.SuccessResponse(product);

            var id = product.Id;

            response.Links.Add(new Link(urlHelper.Action("GetProductById", "Product", new { id })!, "self", "GET"));
            response.Links.Add(new Link(urlHelper.Action("UpdateProduct", "Product", new { id })!, "update_product", "PUT"));
            response.Links.Add(new Link(urlHelper.Action("DeleteProduct", "Product", new { id })!, "delete_product", "DELETE"));

            return response;
        }

        public static ApiResponse<List<Product>> BuildList(List<Product> products, IUrlHelper urlHelper)
        {
            var response = ApiResponse<List<Product>>.SuccessResponse(products);

            foreach (var product in products)
            {
                var id = product.Id;
                response.Links.Add(new Link(urlHelper.Action("GetProductById", "Product", new { id })!, $"product_{id}", "GET"));
            }

            return response;
        }
    }
}