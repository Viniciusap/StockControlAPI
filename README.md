README for StockControlAPI

## Overview
This repository contains the source code for the `ProductController` in the `StockControlAPI`. The controller is built using ASP.NET Core and leverages HATEOAS (Hypermedia as the Engine of Application State) principles for better API navigation and interaction.

## Technologies and Frameworks Used
- **ASP.NET Core**: A cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications.
- **HATEOAS**: A constraint of the REST application architecture that keeps the RESTful style while operating across the hypermedia.

## Controller Details
### ProductController
#### Endpoints
1. **Add Product**
- **URL**: `POST /api/product`
- **Description**: Adds a new product.
- **Request Body**: `ProductDto`
- **Response**:
- **201 Created**: Returns the created product with HATEOAS links.
2. **Get Products**
- **URL**: `GET /api/product`
- **Description**: Retrieves a list of products.
- **Query Parameters**:
- `isActive` (boolean, default `true`)
- `skip` (integer, default `0`)
- `take` (integer, default `10`)
- **Response**:
- **200 OK**: Returns a list of products with HATEOAS links.
3. **Get Product By Id**
- **URL**: `GET /api/product/{id}`
- **Description**: Retrieves a product by its ID.
- **Response**:
- **200 OK**: Returns the product with HATEOAS links.
4. **Update Product**
- **URL**: `PUT /api/product/{id}`
- **Description**: Updates an existing product.
- **Request Body**: `ProductDto`
- **Response**:
- **200 OK**: Returns the updated product with HATEOAS links.
5. **Delete Product**
- **URL**: `DELETE /api/product/{id}`
- **Description**: Deletes a product by its ID.
- **Response**:
- **200 OK**: Returns a success message.
#### HATEOAS Implementation
The API responses are designed to include HATEOAS links, which provide dynamic navigation to related resources. This helps clients interact with the API more intuitively and discover available actions without prior knowledge of the API structure.

## Getting Started
### Prerequisites
Ensure you have the following installed:
- [.NET SDK]([URL]) version 6.0 or higher
### Running the Application
1. Clone the repository:
```
git clone [URL]
```
2. Navigate to the project directory:
```
cd StockControlAPI
```
3. Build the project:
```
dotnet build
```
4. Run the application:
```
dotnet run
```
5. The API will be available at `[URL]`.
