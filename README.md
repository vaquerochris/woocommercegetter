# woocommercegetter
A way to get paginated data out of WooCommerce, and append it to a master JSON file.

This code requires a .NET Core 2.x Solution to use. 

Typically I pull the websiteURL, consumerKey, and consumerSecret from my config.json file in the controller, and I get my afterDate and beforeDate from a form in the view, and then pass them to the WooCommerceService, but you do it however makes sense for you. 
