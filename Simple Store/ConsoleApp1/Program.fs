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


let form = new Form(Text = "Simple Store", Width = 1000, Height = 700, BackColor = Color.White)

let titleLabel = new Label(Text = "Welcome to the Virtual Store", Top = 10, Left = 250, Width = 500, Height = 30)
titleLabel.Font <- new Font("Arial", 16.0f, FontStyle.Bold)
titleLabel.ForeColor <- Color.DarkSlateGray

let productListBox = new ListBox(Top = 50, Left = 20, Width = 400, Height = 200, Font = new Font("Arial", 10.0f))
productListBox.BackColor <- Color.LightCyan
productListBox.BorderStyle <- BorderStyle.FixedSingle

let cartListBox = new ListBox(Top = 50, Left = 550, Width = 400, Height = 200, Font = new Font("Arial", 10.0f))
cartListBox.BackColor <- Color.LightYellow
cartListBox.BorderStyle <- BorderStyle.FixedSingle

let browseButton = new Button(Text = "Browse Products", Top = 320, Left = 250, Width = 500, Height = 40, Font = new Font("Arial", 10.0f))
browseButton.BackColor <- Color.SkyBlue
browseButton.ForeColor <- Color.White
browseButton.FlatStyle <- FlatStyle.Flat
browseButton.FlatAppearance.BorderSize <- 0

let addToCartButton = new Button(Text = "Add to Cart", Top = 380, Left = 250, Width = 500, Height = 40, Font = new Font("Arial", 10.0f))
addToCartButton.BackColor <- Color.MediumSeaGreen
addToCartButton.ForeColor <- Color.White
addToCartButton.FlatStyle <- FlatStyle.Flat
addToCartButton.FlatAppearance.BorderSize <- 0

let removeFromCartButton = new Button(Text = "Remove from Cart", Top = 440, Left = 250, Width = 500, Height = 40, Font = new Font("Arial", 10.0f))
removeFromCartButton.BackColor <- Color.IndianRed
removeFromCartButton.ForeColor <- Color.White
removeFromCartButton.FlatStyle <- FlatStyle.Flat
removeFromCartButton.FlatAppearance.BorderSize <- 0

let checkoutButton = new Button(Text = "Checkout", Top = 500, Left = 250, Width = 500, Height = 40, Font = new Font("Arial", 10.0f))
checkoutButton.BackColor <- Color.Goldenrod
checkoutButton.ForeColor <- Color.White
checkoutButton.FlatStyle <- FlatStyle.Flat
checkoutButton.FlatAppearance.BorderSize <- 0

let resetCartButton = new Button(Text = "Reset Cart", Top = 560, Left = 250, Width = 500, Height = 40, Font = new Font("Arial", 10.0f))
resetCartButton.BackColor <- Color.LightCoral
resetCartButton.ForeColor <- Color.White
resetCartButton.FlatStyle <- FlatStyle.Flat
resetCartButton.FlatAppearance.BorderSize <- 0


form.Controls.Add(titleLabel)
form.Controls.Add(productListBox)
form.Controls.Add(cartListBox)
form.Controls.Add(browseButton)
form.Controls.Add(addToCartButton)
form.Controls.Add(removeFromCartButton)
form.Controls.Add(checkoutButton)
form.Controls.Add(resetCartButton)
form.Controls.Add(totalLabel)

// Gradient Background for the Form
let gradientBrush = new Drawing2D.LinearGradientBrush(
    form.ClientRectangle, 
    Color.LightSkyBlue, 
    Color.White, 
    Drawing2D.LinearGradientMode.Vertical
)

form.Paint.Add(fun e -> 
    e.Graphics.FillRectangle(gradientBrush, form.ClientRectangle)
)
let totalLabel = new Label(Text = "Total: $0.00", Top = 300, Left = 900, Width = 200, Height = 30, Font = new Font("Arial", 10.0f))
totalLabel.ForeColor <- Color.DarkGreen

browseButton.Click.Add(fun _ ->
    productListBox.Items.Clear()
    productCatalog |> List.iter (fun product -> 
        productListBox.Items.Add(sprintf "%s - $%.2f" product.Name product.Price) |> ignore)
)

let mutable cart = []

// Function to update the total cost label
let updateTotal () =
    let total = cart |> List.sumBy (fun product -> product.Price)
    totalLabel.Text <- sprintf "Total: $%.2f" total


addToCartButton.Click.Add(fun _ ->
    let selectedProduct = productListBox.SelectedItem
    if selectedProduct <> null then
        let productName = selectedProduct.ToString().Split([| '-' |]).[0].Trim()
        let productOpt = List.tryFind (fun product -> product.Name = productName) productCatalog
        match productOpt with
        | Some product ->
            cart <- product :: cart
            cartListBox.Items.Add(product.Name)
            updateTotal()
        | None -> ()
)
//function remove from cart button
removeFromCartButton.Click.Add(fun _ ->
    let selectedProduct = cartListBox.SelectedItem
    if selectedProduct <> null then
        let productName = selectedProduct.ToString()
        cart <- List.filter (fun product -> product.Name <> productName) cart
        cartListBox.Items.Remove(productName)
        updateTotal()
)
