using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    void Start() {
        Customer customer = new Customer();
        Waiter waiter = new Waiter();
        Waiter2 waiter2 = new Waiter2();
        customer.Order += waiter.Action;
        customer.Order += waiter.Action2;
        customer.Order += waiter2.Action;
        customer.Do();        
    }   
}
public class OrderEventArgs : EventArgs {
    public string DishName { get; set; }
    public string Size { get; set; }
}
public delegate void OrderEventHandler(Customer customer,OrderEventArgs e);
public class Customer
{
    private OrderEventHandler orderEventHandler;

    public event OrderEventHandler Order {
        add {
            this.orderEventHandler += value;
        }
        remove {
            this.orderEventHandler -= value;
        }
    }
    public void Do() {
        if (this.orderEventHandler !=null)
        {
            OrderEventArgs e = new OrderEventArgs() { DishName = "KFC", Size = "L" };
            this.orderEventHandler.Invoke(this, e);
        }
    }
}
public class Waiter
{
    internal void Action(Customer customer, OrderEventArgs e){Debug.Log(e.DishName+e.Size);}

    internal void Action2(Customer customer, OrderEventArgs e)
    {
        Debug.Log(e.DishName);
    }
}

public class Waiter2
{
    internal void Action(Customer customer, OrderEventArgs e){Debug.Log(e.Size+e.DishName);}
}
