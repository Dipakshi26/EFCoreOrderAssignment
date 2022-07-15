using ItemCustomerData.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItemCustomerData
{
    public class CrudManager
    {
        DemoDbContext demoDbContext = new DemoDbContext();
                
            #region customer 
            public void AddCustomer()
        {
            CustomerMaster customerMaster = new CustomerMaster();

        Add:

            Console.WriteLine(" Enter Customer's First Name to Add");
            customerMaster.Customer_FName = Console.ReadLine();

            Console.WriteLine(" Enter Customer's Last Name to Add");
            customerMaster.Customer_LName = Console.ReadLine();

            Console.WriteLine(" Enter Customer's Phone No to Add");
            customerMaster.Customer_Phone = int.Parse(Console.ReadLine());


        email:

            Console.WriteLine(" Enter Email to Add");
            customerMaster.Customer_Email = Console.ReadLine();

            if (!mailValidation(customerMaster.Customer_Email))

            {
                Console.WriteLine("Enter Some Valid  Email");
                goto email;

            }
            if (customerMaster.Customer_FName != "" && customerMaster.Customer_LName != "" && customerMaster.Customer_Email != "")
            {
                if (!CustomerExistOrNot(customerMaster.Customer_Email) && mailValidation(customerMaster.Customer_Email))
                {
                    demoDbContext.CustomerMasters.Add(customerMaster);
                    demoDbContext.SaveChanges();

                    emailsend(customerMaster.Customer_Email, customerMaster.Customer_FName, customerMaster.Customer_LName);
                }
                else
                {
                    Console.WriteLine("Please enter a Valid Email");
                }
            }
            else
            {
                Console.WriteLine("Email can't be null");
            }
        }
        public List<CustomerMaster> GetAllcustomers()
        {
            var item = demoDbContext.CustomerMasters.ToList();
            return item;
        }

        public void UpdateCustomer()
        {
            CustomerMaster customerMaster = new CustomerMaster();
            string? FName, LName;
            int Phone;
        Update:
        email1:

            Console.WriteLine(" Enter Email to Add");
            customerMaster.Customer_Email = Console.ReadLine();

            if (!mailValidation(customerMaster.Customer_Email))

            {
                Console.WriteLine("Enter Some   Email..It can not be null");
                goto email1;

            }
            if (customerMaster.Customer_Email != "")
            {
                if (!CustomerExistOrNot(customerMaster.Customer_Email) && mailValidation(customerMaster.Customer_Email))
                {


                    Console.WriteLine(" Enter Customer's First Name to Update");
                    FName = Console.ReadLine();

                    Console.WriteLine(" Enter Customer's Last Name to Update");
                    LName = Console.ReadLine();

                    Console.WriteLine(" Enter Customer's Phone No to Update");
                    Phone = int.Parse(Console.ReadLine());

                    var customer = demoDbContext.CustomerMasters.Where(x => x.Customer_Email == customerMaster.Customer_Email).FirstOrDefault();
                    if (customer == null)
                    {
                        throw new Exception($"customer with Customer_Email:{customerMaster.Customer_Email} Not Found");
                    }
                    customer.Customer_FName = FName;
                    customer.Customer_LName = LName;
                    customer.Customer_Phone = Phone;


                    // Entity state : Modified
                    demoDbContext.CustomerMasters.Update(customer);

                    // This issues insert statement
                    demoDbContext.SaveChanges();

                }
                else
                {
                    Console.WriteLine("Please enter a Valid Email");
                    goto Update;
                }
            }
            else
            {
                Console.WriteLine("Email can't be null");
                goto Update;
            }
        }
        public void DeleteCustomer()
        {
            CustomerMaster customerMaster = new CustomerMaster();

        top2:

            Console.WriteLine("Enter the Email To be Delete");
            string? Email = Console.ReadLine();



            if (Email == "")

            {
                Console.WriteLine("Enter Some Email..");
                goto top2;

            }
            else
            {
                if (CustomerExistOrNot(Email))
                {
                    var customer = demoDbContext.CustomerMasters.Where(x => x.Customer_Email == customerMaster.Customer_Email).FirstOrDefault();
                    if (customer == null)
                    {
                        throw new Exception($"customer with Customer_Email:{customerMaster.Customer_Email} Not Found");
                    }
                    // Entity state : Deleted
                    demoDbContext.CustomerMasters.Remove(customer);

                    // This issues insert statement
                    demoDbContext.SaveChanges();

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Deleted Succesfully");
                }
                else
                {
                    Console.WriteLine("Please enter Email to delete");
                    goto top2;
                }
            }
        }
        public bool CustomerExistOrNot(string Email)
        {
            var yesOrNot = demoDbContext.CustomerMasters.Where(customer => customer.Customer_Email == Email).FirstOrDefault();
            if (yesOrNot != null)
            {
                return true;
            }
            return false;
        }
        public bool mailValidation(string Email)
        {

            Regex eml = new Regex(@"^[a-zA-Z]+[._]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.]+[a-zA-Z]{2,3}){0,1}");
            Match m = eml.Match(Email);
            if (m.Success)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        void emailsend(string to, string firstname, string lastname)
        {
            string from = "dipakshisaxena26@gmail.com";
            string password = "xycbdulnhtdwinsk";
            string subject = "Welcome Dear Customer";
            string body = "<h1>Dear, " + firstname + " " + lastname + " </h1>\nThanks for registering with us..Have a good day";
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Console.WriteLine("Mail Sent Succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            #endregion
        }
        #region ItemMaster
        public List<ItemMaster> GetAllItems()
        {
            var item = demoDbContext.ItemMasters.ToList();
            return item;
        }


        public void AddItem()
        {
            ItemMaster itemMaster = new ItemMaster();
        Top:

            try
            {
                Console.WriteLine(" Enter Item Name to Add");
                itemMaster.Item_Name = Console.ReadLine();
                Console.WriteLine("Enter Price of Item");
                itemMaster.Item_Rate = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Quantity Of Item");
                itemMaster.Item_Qty = int.Parse(Console.ReadLine());


                if (itemMaster.Item_Name != "")
                {
                    if (!ItemExistOrNot(itemMaster.Item_Name))
                    {

                        demoDbContext.ItemMasters.Add(itemMaster);
                        demoDbContext.SaveChanges();


                        Console.WriteLine("Added Succesfully");
                    }

                    else
                    {
                        Console.WriteLine(" This Item Exits Try with another One");
                        goto Top;
                    }
                }

                else
                {
                    Console.WriteLine(" Please Enter the  Value..and try again");
                    goto Top;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteItem()
        {
        top2:

            Console.WriteLine("Enter the Item Name To be Delete");
            string? ItemName = Console.ReadLine();

            if (ItemName == "")

            {
                Console.WriteLine("Enter Some Name");
                goto top2;

            }
            else
            {
                if (ItemExistOrNot(ItemName))
                {
                    var item = demoDbContext.ItemMasters.Where(x => x.Item_Name == ItemName).FirstOrDefault();
                    if (item == null)
                    {
                        throw new Exception($"Item with name:{ItemName} Not Found");
                    }

                    // Entity state : Deleted
                    demoDbContext.ItemMasters.Remove(item);

                    // This issues insert statement
                    demoDbContext.SaveChanges();

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Deleted Succesfully");
                }
                else
                {
                    Console.WriteLine("Please enter a Valid name to delete");
                    goto top2;
                }
            }
        }

        public void UpdateItem()
        {
            ItemMaster itemMaster = new ItemMaster();

            int? modifiedRate;
            int? modifiedQty;

            Console.WriteLine("Enter the name of item You want to update");
            itemMaster.Item_Name = Console.ReadLine();

        top1:

            if (itemMaster.Item_Name != "")
            {

                if (ItemExistOrNot(itemMaster.Item_Name))
                {

                    Console.WriteLine("Enter Price of Item to update");
                    modifiedRate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Quantity Of Item");
                    modifiedQty = int.Parse(Console.ReadLine());

                    var item = demoDbContext.ItemMasters.Where(x => x.Item_Name == itemMaster.Item_Name).FirstOrDefault();
                    if (item == null)
                    {
                        throw new Exception($"item with Item_Name:{itemMaster.Item_Name} Not Found");
                    }


                    item.Item_Rate = (int)modifiedRate;
                    item.Item_Qty = (int)modifiedQty;


                    // Entity state : Modified
                    demoDbContext.ItemMasters.Update(item);

                    // This issues insert statement
                    demoDbContext.SaveChanges();
                }

                else
                {
                    Console.WriteLine("Item Does Not Match Please Enter some valid item name");
                    goto top1;
                }
            }

            else
            {
                Console.WriteLine("Please Enter Some Value to update");
                goto top1;
            }
        }
        private bool ItemExistOrNot(string Item_Name)
        {


            var yesOrNot = demoDbContext.ItemMasters.Where(item => item.Item_Name == Item_Name).FirstOrDefault();
            if (yesOrNot != null)
            {
                return true;
            }

            return false;
        }
        #endregion

    }

}


