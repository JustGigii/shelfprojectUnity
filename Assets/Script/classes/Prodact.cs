﻿using System.Collections.Generic;



public class Product
{
    public string name { get; set; }

    public string description { get; set; }

    public float price { get; set; }
}
public class ProductList
{
    public Product[] products;
}