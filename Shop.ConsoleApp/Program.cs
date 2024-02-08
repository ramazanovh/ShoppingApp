

using EFProjectApp.DataAccess;
using Shop.Business.Services;
using Shop.Business.Utilities.Exceptions;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;

AppDbContext _contex = new AppDbContext();
Console.WriteLine(" .d8888b.  888                           8888888b.                   d8b                   888    \r\nd88P  Y88b 888                           888   Y88b                  Y8P                   888    \r\nY88b.      888                           888    888                                        888    \r\n \"Y888b.   88888b.   .d88b.  88888b.     888   d88P 888d888 .d88b.  8888  .d88b.   .d8888b 888888 \r\n    \"Y88b. 888 \"88b d88\"\"88b 888 \"88b    8888888P\"  888P\"  d88\"\"88b \"888 d8P  Y8b d88P\"    888    \r\n      \"888 888  888 888  888 888  888    888        888    888  888  888 88888888 888      888    \r\nY88b  d88P 888  888 Y88..88P 888 d88P    888        888    Y88..88P  888 Y8b.     Y88b.    Y88b.  \r\n \"Y8888P\"  888  888  \"Y88P\"  88888P\"     888        888     \"Y88P\"   888  \"Y8888   \"Y8888P  \"Y888 \r\n                             888                                     888                          \r\n                             888                                    d88P                          \r\n                             888                                  888P\"  ");
UserService userService = new UserService(new AppDbContext());
ProductService productService = new ProductService(new AppDbContext());
CardService cardService = new CardService(new AppDbContext());
WalletService walletService = new WalletService(new AppDbContext(), cardService);
ProductInvoiceService productInvoiceService = new ProductInvoiceService(new AppDbContext());
InvoiceService invoiceService = new InvoiceService(new AppDbContext(), cardService, productInvoiceService);
BrandService brandService = new BrandService(new AppDbContext());
CategoryService categoryService = new CategoryService(new AppDbContext());

bool isContinue = true;
while (isContinue)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n1) Login\n2) Register\n3) Exit");
    var option = Console.ReadLine();
    Console.Write("Choose the option: ");
    Console.ResetColor();

    if (option == "1")
    {
        var (loginSuccess, UsernameOrEmail) = AttemptLogin(userService);
        if (loginSuccess)
        {
            Console.WriteLine("Invalid credentials. Please try again.");
        }
        else
        {
            bool isAdmin = userService.IsUserAdmin(UsernameOrEmail);

            if (isAdmin)
            {
                AdminDesk(userService, productService, brandService, categoryService);
            }
            else
            {
                UserDesk(userService, productService, cardService, walletService, productInvoiceService, invoiceService);
            }
        }
    }
    else if (option == "2")
    {
        var registration = await AttemptRegistration(userService);
        if (registration)
        {
            Console.WriteLine("Registration successful. Please login.");
        }
        else
        {
            Console.WriteLine("Registration failed. Please try again.");
        }
    }
    else if (option == "3")
    {
        Console.WriteLine("Exiting App.");
        isContinue = false;
    }
    else
    {
        Console.WriteLine("Wrong option. Please try again.");
    }

    static async Task<bool> AttemptRegistration(UserService userService)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = ReadPassword();

        var registration = await userService.Register(username, email, password);
        return registration;
    }

    static async Task<(bool, string)> AttemptLogin(UserService userService)
    {
        Console.Write("Enter username or email: ");
        var usernameOrEmail = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = ReadPassword();

        var loginSuccess = await userService.Login(usernameOrEmail, password);
        return (loginSuccess, usernameOrEmail);
    }

    static string ReadPassword()
    {
        var password = "0404";
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }
        return password;
    }

    static async Task AdminDesk(UserService UserService, ProductService productService, BrandService brandService, CategoryService categoryService)
    {
        bool adminSession = true;
        while (adminSession)
        {
            Console.WriteLine("\n Admin Desk ");
            Console.WriteLine(
                "1) Create User\n" +
                "2) Update User\n" +
                "3) Delete User\n" +
                "4) Get All Users\n" +
                "5) Activate User\n" +
                "6) Deactivate User\n" +
                "7) Create Product\n" +
                "8) Update Product\n" +
                "9) Delete Product\n" +
                "10) Activate Product\n" +
                "11) Deactivate Product\n" +
                "12) Get All Products\n" +
                "13) CreateBrand\n" +
                "14) CreateCategory\n" +
                "15) Exit\n" +
                "Choose an option: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int option) && Enum.IsDefined(typeof(AdminOption), option))
            {
                switch ((AdminOption)option)
                {
                    case AdminOption.CreateUser:
                        Console.Write("Enter name: ");
                        var name = Console.ReadLine();
                        Console.Write("Enter username: ");
                        var userName = Console.ReadLine();
                        Console.Write("Enter password: ");
                        var password = Console.ReadLine();
                        Console.Write("Enter email: ");
                        var email = Console.ReadLine();
                        Console.Write("Enter phone: ");
                        var UserPhone = Console.ReadLine();
                        Console.Write("Is Admin (true/false): ");
                        bool isAdmin = bool.Parse(Console.ReadLine() ?? "false");

                        var createUserResult = await UserService.CreateUser(name,
                                                                            userName,
                                                                            password,
                                                                            email,
                                                                            UserPhone,
                                                                            isAdmin);

                        if (createUserResult == true)
                        {
                            Console.WriteLine($"User '{name}' created successfully.");
                        }
                        else if (createUserResult == null)
                        {
                            Console.WriteLine("A user with the given username or email already exists.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to create user.");
                        }
                        break;

                    case AdminOption.UpdateProfile:
                        Console.Write("Enter the user ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int userId))
                        {
                            Console.Write("Enter new username (leave blank to keep unchanged): ");
                            var newUsername = Console.ReadLine();
                            Console.Write("Enter new email (leave blank to keep unchanged): ");
                            var newEmail = Console.ReadLine();
                            Console.Write("Enter new password (leave blank to keep unchanged): ");
                            var newPassword = Console.ReadLine();
                            Console.Write("Enter new name (leave blank to keep unchanged): ");
                            var newName = Console.ReadLine();
                            Console.Write("Enter new phone (leave blank to keep unchanged): ");
                            var newPhone = Console.ReadLine();

                  

                            if (updateResult == true)
                            {
                                Console.WriteLine("User updated successful.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update user or user not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid user ID.");
                        }
                        break;

                    case AdminOption.DeleteUser:
                        Console.Write("Enter the ID of the user to delete: ");
                        if (!int.TryParse(Console.ReadLine(), out int deleteUserId))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid user ID.");
                            break;
                        }
                        Console.Write($"Are you sure you want to permanently delete the user with ID {deleteUserId}? (yes/no): ");
                        var confirmation = Console.ReadLine();
                        if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            var deleteResult = UserService.DeleteUser(deleteUserId);
                            if (deleteResult)
                            {
                                Console.WriteLine("User deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete user or user not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Deletion cancelled.");
                        }
                        break;

                    case AdminOption.GetAllUsers:
                        var allUsers = UserService.GetAllUsers();

                        if (allUsers.Count > 0)
                        {
                            Console.WriteLine("All Users:");
                            foreach (var user in allUsers)
                            {
                                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Phone: {user.Phone}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No users found.");
                        }
                        break;

                    case AdminOption.ActivateProfile:
                        Console.Write("Enter the ID of the user to activate: ");
                        var activateUserId = Console.ReadLine();

                        if (int.TryParse(activateUserId, out int idToActivate))
                        {
                            var activationResult = UserService.ActivateProfile(idToActivate);

                            if (activationResult)
                            {
                                Console.WriteLine("User activated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to activate user or user not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid user ID.");
                        }
                        break;

                    case AdminOption.DeactivateProfile:
                        Console.Write("Enter the ID of the user to deactivate: ");
                        var deactivateUserId = Console.ReadLine();

                        if (int.TryParse(deactivateUserId, out int idToDeactivate))
                        {
                            var deactivationResult = UserService.DeactivateProfile(idToDeactivate);

                            if (deactivationResult)
                            {
                                Console.WriteLine("User deactivated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to deactivate user or user not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid user ID.");
                        }
                        break;
                    case AdminOption.CreateProduct:
                        Console.Write("Enter product name: ");
                        var productName = Console.ReadLine();
                        Console.Write("Enter product description: ");
                        var productDescription = Console.ReadLine();
                        Console.Write("Enter product price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal productPrice))
                        {
                            Console.WriteLine("Invalid price format. Please enter a valid decimal.");
                            break;
                        }
                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int productQuantity))
                        {
                            Console.WriteLine("Invalid quantity format. Please enter a valid integer.");
                            break;
                        }
                        Console.Write("Enter category ID: ");
                        if (int.TryParse(Console.ReadLine(), out int productCategoryId))
                        {
                            Console.WriteLine("Invalid category ID format. Please enter a valid integer.");
                            break;
                        }
                        Console.Write("Enter brand ID: ");
                        if (int.TryParse(Console.ReadLine(), out int productBrandId))
                        {
                            Console.WriteLine("Invalid brand format. Please enter a valid integer");
                            break;
                        }
                        Console.Write("Enter discount ID (leave blank if none): ");
                        if (int.TryParse(Console.ReadLine(), out int productDiscountId))
                        {
                            productDiscountId = 0;
                        }

                        try
                        {
                            productService.CreateProduct(productName, productDescription, productPrice, productQuantity, productCategoryId, productBrandId, productDiscountId);
                            Console.WriteLine("Product created successful.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case AdminOption.UpdateProduct:
                        Console.Write("Enter the product ID to update: ");
                        if (int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.Write("Enter new product name (leave blank to keep unchanged): ");
                            var newName = Console.ReadLine();
                            Console.Write("Enter new product description (leave blank to keep unchanged): ");
                            var newDescription = Console.ReadLine();

                            Console.Write("Enter new product price (leave blank to keep unchanged): ");
                            var priceInput = Console.ReadLine();
                            decimal? newPrice = !string.IsNullOrWhiteSpace(priceInput) ? decimal.Parse(priceInput) : null;

                            Console.Write("Enter new product quantity available (leave blank to keep unchanged): ");
                            var quantityInput = Console.ReadLine();
                            int? newQuantityAvailable = !string.IsNullOrWhiteSpace(quantityInput) ? int.Parse(quantityInput) : null;

                            Console.Write("Enter new product category ID (leave blank to keep unchanged): ");
                            var categoryIdInput = Console.ReadLine();
                            int? newCategoryId = !string.IsNullOrWhiteSpace(categoryIdInput) ? int.Parse(categoryIdInput) : null;

                            Console.Write("Enter new product brand ID (leave blank to keep unchanged): ");
                            var brandIdInput = Console.ReadLine();
                            int? newBrandId = !string.IsNullOrWhiteSpace(brandIdInput) ? int.Parse(brandIdInput) : null;

                            Console.Write("Enter new product discount ID (leave blank to keep unchanged): ");
                            var discountIdInput = Console.ReadLine();
                            int? newDiscountId = !string.IsNullOrWhiteSpace(discountIdInput) ? int.Parse(discountIdInput) : null;

                            var updateResult = await productService.UpdateProduct(productId, newName, newDescription, newPrice, newQuantity, newCategoryId, newBrandId, newDiscountId);

                            if (updateResult == true)
                            {
                                Console.WriteLine("Product updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update product or product not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID.");
                        }
                        break;

                    case AdminOption.DeleteProduct:
                        Console.Write("Enter the product ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int productToDeleteId))
                        {
                            Console.Write($"Are you sure you want to permanently delete the product with ID {productToDeleteId}? (yes/no): ");
                            var productDeleteConfirmation = Console.ReadLine();
                            if (productDeleteConfirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
                            {
                                var productExists = productService.(productToDeleteId);
                                if (productExists)
                                {
                                    productService.DeleteProduct(productToDeleteId);
                                    Console.WriteLine("Product deleted successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Deletion cancelled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID.");
                        }
                        break;

                    case AdminOption.ActivateProduct:
                        Console.Write("Enter the product ID to activate: ");
                        if (int.TryParse(Console.ReadLine(), out int activateProductId))
                        {
                            var activateResult = await productService.ActivateProduct(activateProductId);
                            if (activateResult)
                            {
                                Console.WriteLine("Product activated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to activate product or product not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID.");
                        }
                        break;

                    case AdminOption.DeactivateProduct:
                        Console.Write("Enter the product ID to deactivate: ");
                        if (int.TryParse(Console.ReadLine(), out int deactivateProductId))
                        {
                            var deactivateResult = productService.DeactivateProduct(deactivateProductId);
                            if (deactivateResult)
                            {
                                Console.WriteLine("Product deactivated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to deactivate product or product not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID.");
                        }
                        break;

                    case AdminOption.GetAllProducts:
                        try
                        {
                            var allProducts = productService.GetAllProducts();

                            Console.WriteLine("\n--- All Products ---");
                            foreach (var product in allProducts)
                            {
                                Console.WriteLine($"ID: {product.Id}");
                                Console.WriteLine($"Name: {product.Name}");
                                Console.WriteLine($"Description: {product.Description}");
                                Console.WriteLine($"Price: {product.Price:C}");
                                Console.WriteLine($"Quantity Available: {product.QuantityAvailable}");
                                Console.WriteLine($"Category ID: {product.CategoryId}");
                                Console.WriteLine($"Brand ID: {product.BrandId}");
                                Console.WriteLine($"Discount ID: {product.DiscountId}");
                                Console.WriteLine($"Created: {product.Created}");
                                Console.WriteLine($"Updated: {product.Updated}");
                                Console.WriteLine("---------------------------------");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while getting all products: {ex.Message}");
                        }
                        break;

                    case AdminOption.CreateBrand:
                        Console.Write("Enter the name of the brand: ");
                        string? brandName = Console.ReadLine();
                        try
                        {
                            var createdBrand = await brandService.CreateBrandAsync(brandName);
                            Console.WriteLine($"Brand '{createdBrand.Name}' created successful with ID: {createdBrand.Id}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error creating brand: {ex.Message}");
                        }
                        break;

                    case AdminOption.CreateCategory:
                        Console.Write("Enter the name category: ");
                        string? categoryName = Console.ReadLine();

                        try
                        {
                            Category newCategory = categoryService.CreateCategory(categoryName);
                            Console.WriteLine($"Category '{newCategory.Name}' created successful.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case AdminOption.Exit:
                        adminSession = false;
                        Console.WriteLine("Log out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
    static async Task UserDesk(UserService userService, ProductService productService, CardService cardService, WalletService walletService, ProductInvoiceService invoiceItemService, InvoiceService invoiceService)
    {
        bool userSession = true;
        while (userSession)
        {
            Console.WriteLine("\n--- User Panel ---");
            Console.Write(
                "1) Get User By Id\n" +
                "2) Product Exists\n" +
                "3) Get All Products\n" +
                "4) Get Product By Id\n" +
                "5) Get Card By Id\n" +
                "6) Get All Cards\n" +
                "7) Create Card\n" +
                "8) Update Card\n" +
                "9) Delete Card\n" +
                "10) Increase Card Balance\n" +
                "11) Decrease Card Balance\n" +
                "12) Check if Card Exists\n" +
                "13) Get Card Balance\n" +
                "14) Get Wallet By Id\n" +
                "15) Get All Wallets\n" +
                "16) Create Wallet\n" +
                "17) Update Wallet\n" +
                "18) Delete Wallet\n" +
                "19) Get Wallet Balance\n" +
                "20) Increase Wallet Balance\n" +
                "21) CreateInvoiceItem\n" +
                "22) CreateInvoice\n" +
                "22) Logout\n" +
                "Choose an option: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int option) && Enum.IsDefined(typeof(UserOption), option))
            {
                switch ((UserOption)option)
                {
                    case UserOption.GetAllProducts:
                        try
                        {
                            var products = productService.GetAllProducts();
                            Console.WriteLine("\n--- All Products ---");
                            foreach (var product in products)
                            {
                                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while retrieving products: {ex.Message}");
                        }
                        break;

                    case UserOption.GetAllCards:
                        try
                        {
                            var allCards = cardService.GetAllCards();

                            Console.WriteLine("\n--- All Cards ---");
                            foreach (var card in allCards)
                            {
                                Console.WriteLine($"ID: {card.Id}");
                                Console.WriteLine($"Card Number: {card.CardNumber}");
                                Console.WriteLine($"Card Holder Name: {card.CardHolderName}");
                                Console.WriteLine($"CVC: {card.Cvc}");
                                Console.WriteLine($"User ID: {card.UserId}");
                                Console.WriteLine($"Balance: {card.Balance:C}");
                                Console.WriteLine($"Wallet ID: {card.WalletId}");
                                Console.WriteLine("---------------------------------");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while getting all cards: {ex.Message}");
                        }
                        break;

                    case UserOption.CreateCard:
                        Console.Write("Enter card number: ");
                        var cardNumber = Console.ReadLine();
                        Console.Write("Enter card holder name: ");
                        var cardHolderName = Console.ReadLine();
                        Console.Write("Enter CVV: ");
                        if (int.TryParse(Console.ReadLine(), out int cvc))
                        {
                            Console.WriteLine("Invalid CVV format. Please enter a valid integer.");
                            break;
                        }
                        Console.Write("Enter user ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int userId))
                        {
                            Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
                            break;
                        }
                        Console.Write("Enter balance: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal balance))
                        {
                            Console.WriteLine("Invalid balance format. Please enter a valid decimal.");
                            break;
                        }
                        Console.Write("Enter wallet ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int walletId))
                        {
                            Console.WriteLine("Invalid wallet ID format. Please enter a valid integer.");
                            break;
                        }

                        try
                        {
                            var newCard = new Card(cardNumber, cardHolderName, cvc, userId, balance, walletId);
                            cardService.CreateCard(newCard);
                            Console.WriteLine("Card created successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case UserOption.UpdateCard:
                        Console.Write("Enter the ID of the card to update: ");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToUpdate))
                        {
                            Console.Write("Enter new card number (leave blank to keep unchanged): ");
                            var newCardNumberInput = Console.ReadLine();
                            string? newCardNumber = string.IsNullOrWhiteSpace(newCardNumberInput) ? null : newCardNumberInput;

                            Console.Write("Enter new card holder name (leave blank to keep unchanged): ");
                            var newCardHolderNameInput = Console.ReadLine();
                            string? newCardHolderName = string.IsNullOrWhiteSpace(newCardHolderNameInput) ? null : newCardHolderNameInput;

                            Console.Write("Enter new CVV (leave blank to keep unchanged): ");
                            var cvcInput = Console.ReadLine();
                            int? newCvc = null;
                            if (string.IsNullOrWhiteSpace(cvcInput))
                            {
                                newCvc = int.Parse(cvcInput);
                            }

                            try
                            {
                                cardService.UpdateCard(cardIdToUpdate, newCardNumber, newCardHolderName, newCvc);
                                Console.WriteLine("Card updated successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid card ID.");
                        }
                        break;

                    case UserOption.DeleteCard:
                        Console.Write("Enter the ID of the card to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToDelete))
                        {
                            // Confirmation before deletion
                            Console.Write($"Are you sure you want to permanently delete the card with ID {cardIdToDelete}? (yes/no): ");
                            var confirmation = Console.ReadLine();
                            if (confirmation.Equals("yes", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    cardService.DeleteCard(cardIdToDelete);
                                    Console.WriteLine("Card deleted successful.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Deletion cancelled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid card ID.");
                        }
                        break;
                    case UserOption.CardExists:
                        Console.Write("Enter the ID the card to check: ");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToCheck))
                        {
                            try
                            {
                                var exists = await cardService.CardExists(cardIdToCheck);
                                Console.WriteLine($"Card with ID {cardIdToCheck} {(exists ? "exists." : "does not exist.")}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid card ID.");
                        }
                        break;

                    case UserOption.GetCardBalance:
                        Console.Write("Enter the ID of the card to get balance: ");
                        if (int.TryParse(Console.ReadLine(), out int cardIdToGetBalance))
                        {
                            try
                            {
                                var cardBalance = await cardService.GetCardBalanceAsync(cardIdToGetBalance);
                                Console.WriteLine($"Balance of card with ID {cardIdToGetBalance}: {cardBalance}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid card ID.");
                        }
                        break;
                    case UserOption.GetAllWallets:
                        try
                        {
                            var wallets = walletService.GetAllWallets();
                            Console.WriteLine("All Wallets:");
                            foreach (var wallet in wallets)
                            {
                                Console.WriteLine($"ID: {wallet.Id}, User ID: {wallet.UserId}, Balance: {wallet.Balance}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case UserOption.CreateWallet:
                        Console.Write("Enter user ID: ");
                        if (int.TryParse(Console.ReadLine(), out int userID))
                        {
                            try
                            {
                                var wallet = new Wallet { UserId = userID };
                                bool created = WalletService.CreateWallet(wallet, userID);
                                if (created)
                                {
                                    Console.WriteLine("Wallet created successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create wallet.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid user ID format. Please enter a valid integer.");
                        }
                        break;

                    case UserOption.UpdateWallet:
                        Console.Write("Enter the ID of the wallet to update: ");
                        if (int.TryParse(Console.ReadLine(), out int wAlletID))
                        {
                            Console.Write("Enter the new user ID: ");
                            if (int.TryParse(Console.ReadLine(), out int uSerID))
                            {
                                try
                                {
                                    bool isUpdated = walletService.UpdateWallet(wAlletID, uSerID);
                                    if (isUpdated)
                                    {
                                        Console.WriteLine("Wallet user ID updated successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to update wallet user ID. Wallet not found.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid user ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid wallet ID.");
                        }
                        break;

                    case UserOption.DeleteWallet:
                        Console.Write("Enter the ID of the wallet to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int walletIdToDelete))
                        {
                            try
                            {
                                bool success = walletService.DeleteWallet(walletIdToDelete);
                                if (success)
                                {
                                    Console.WriteLine($"Wallet with ID {walletIdToDelete} deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine($"Failed to delete wallet with ID {walletIdToDelete}.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid wallet ID.");
                        }
                        break;

                    case UserOption.GetWalletBalance:
                        Console.Write("Enter your user ID: ");
                        if (int.TryParse(Console.ReadLine(), out int userIdForBalance))
                        {
                            try
                            {
                                var Balance = walletService.GetWalletBalance(userIdForBalance);
                                Console.WriteLine($"Your wallet balance: {Balance}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid user ID.");
                        }
                        break;

                    case UserOption.IncreaseWalletBalance:
                        Console.Write("Enter the ID of the wallet: ");
                        if (int.TryParse(Console.ReadLine(), out int WalletId))
                        {
                            Console.Write("Enter the ID of the card: ");
                            if (int.TryParse(Console.ReadLine(), out int cardIdd))
                            {
                                Console.Write("Enter the amount to increase: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                                {
                                    try
                                    {
                                        walletService.IncreaseWalletBalance(WalletId, cardIdd, amount);
                                        Console.WriteLine("Wallet balance increased successfully.");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error: {ex.Message}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid amount.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid card ID.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid wallet ID.");
                        }
                        break;

                    case UserOption.CreateProductInvoice:
                        Console.Write("Enter the ID of the product: ");
                        if (int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.Write("Enter the Count: ");
                            if (int.TryParse(Console.ReadLine(), out int quantity))
                            {
                                try
                                {
                                    var newProductInvoices = new ProductInvoices
                                    {
                                        ProductId = productId,
                                        ProductCount = quantity
                                    };
                                    bool success = ProductInvoiceService.CreateProductInvoice(newProductInvoices);
                                    if (success)
                                    {
                                        Console.WriteLine("Product Invoice created successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to create Product Invoice.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid quantity entered.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID entered.");
                        }
                        break;

                    case UserOption.CreateInvoice:
                        Console.WriteLine("Enter the ID the invoice:");
                        string inputIds = Console.ReadLine();
                        var ProductInvoice = inputIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(id => int.Parse(id))
                                                      .ToList();

                        Console.Write("Enter the ID of the card to pay with: ");
                        int cardId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the ID of the user placing the order: ");
                        int userIdd = int.Parse(Console.ReadLine());

                        try
                        {
                            bool success = invoiceService.CreateInvoice(ProductInvoice, cardId, userIdd);
                            if (success)
                            {
                                Console.WriteLine("Invoice created successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to create invoice.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case UserOption.Exist:
                        userSession = false;
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}
