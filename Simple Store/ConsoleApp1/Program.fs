open System
open System.Windows.Forms
open System.Drawing



type Product = {
    Name: string
    Price: float
    Description: string
}


let productCatalog = [
    { Name = "Laptop"; Price = 1000.0; Description = "A high-performance laptop" }
    { Name = "Smartphone"; Price = 500.0; Description = "Latest model smartphone" }
    { Name = "Headphones"; Price = 150.0; Description = "Noise-cancelling headphones" }
    { Name = "Keyboard"; Price = 50.0; Description = "Mechanical keyboard" }
]


