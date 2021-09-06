using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using tovuti_api.Models;

namespace tovuti_api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Tovuti : ITovuti
    {
        AddLog log = new AddLog();

        public decimal CreateUser(UserParameters user)
        {
            decimal userid = 0;


            using (var dbContext = new ApplicationDbContext("public"))
            {
                User iuser = new User
                {
                    pass = Hash.Get(user.Password, Hash.DefaultHashType, Hash.DefaultEncoding),
                    //pass = user.Password,
                    username = user.username,
                    // Status = true,
                    //LastActive = DateTime.Now

                };

                dbContext.Users.Add(iuser);
                dbContext.SaveChanges();
                userid = dbContext.Users.Where(u => u.username == user.username).SingleOrDefault().id;
            }

            return userid;

        }

        public Response register(UserParameters body)
        {
            string status = "";
            Response response = new Response();
            using (var dbContext = new ApplicationDbContext("public"))
            {


                try
                {
                    var customerindb = dbContext.Users.Where(u => u.username == body.username).SingleOrDefault();
                    if (customerindb == null)
                    {
                        decimal userid = 0;

                        userid = CreateUser(body);
                        status = "The customer is has been successfully registered";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The customer is already registered";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }





                }
                catch (Exception ex)
                {
                    log.LogError(ex.Message);
                    status = "Dear customer, we are experiencing technical challenges please try later";
                    response.ResponseCode = 0;
                    response.ResponseDetails = status;

                }
            }
            return response;
        }

        public UserToken Authenticate(Credentials creds)
        {
            UserToken usertoken = new UserToken();

            try {
                using (var dbContext = new ApplicationDbContext("public"))
                {
                    ICredentialsValidator validator = new CredentialsValidator(dbContext);
                    if (validator.IsValid(creds))
                    {
                        var user = dbContext.Users.Where(u => u.username.ToLower() == creds.Username.ToLower()).SingleOrDefault();
                        string Token = new TokenBuilder(dbContext).Build(creds);
                        usertoken.Token = Token;
                        usertoken.Username = creds.Username;
                        usertoken.uid = user.id;


                        var tokendetails = dbContext.Tokens.Where(u => u.Text == Token).SingleOrDefault();

                        if (tokendetails != null)
                            usertoken.CreateDate = tokendetails.CreateDate.ToString();

                        user.LastActive = DateTime.Now;
                        dbContext.SaveChanges();


                        //this.GenerateOTP(user.Id, user.PhoneNumber);


                        // AddActivityLog(creds.Username, "Logged sucessfully");
                    }


                }
            }
            catch (Exception Ex)
            {

            }
            return usertoken;
        }

        public string Test(string Param)
        {

            return Param;
        }

        public List<User> AllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (NpgsqlConnection conn = Common.GetConnection())
                {


                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM  public.users", conn)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 1000000

                    };

                    NpgsqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        User user = new User
                        {
                            id = Convert.ToInt32(dr[0].ToString()),
                            username = dr[1].ToString(),
                            pass = dr[2].ToString(),
                        };
                        users.Add(user);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return users;
        }

        public Response productCategory(Category body)
        {
            string status = "";
            Response response = new Response();
            decimal cat_id = 0;

            try { 
            using (var dbContext = new ApplicationDbContext("public"))
            {
               var Exists = dbContext.Categories.Where(u => u.cname == body.cname).SingleOrDefault();

                
                    if (Exists == null)
                    {
                        category cat = new category
                        {
                            // cid = user.Customertype,
                            created_by = body.created_by,
                            cname = body.cname,
                            // Status = true,
                            //LastActive = DateTime.Now

                        };

                        dbContext.Categories.Add(cat);
                        dbContext.SaveChanges();
                        cat_id = dbContext.Categories.Where(u => u.cname == body.cname).SingleOrDefault().cid;
                    }
                    else
                    {
                        cat_id = Exists.cid;
                    }

                    if (cat_id != 0 && Exists == null)
                    {
                        status = "The category '" + body.cname + "' has been successfully added";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else if (cat_id != 0 && Exists != null)
                    {
                        status = "The category '" + body.cname + "' already exists";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The category '" + body.cname + "' was not added. Please try again";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }
                }

              

            }
            catch(Exception Ex)
            {
            
            }
           // return userid;
            return response;

        }


            public List<category> productCategories()
        {
            List<category> categories = new List<category>();

            try
            {
                using (NpgsqlConnection conn = Common.GetConnection())
                {


                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM  public.vw_prodcategories", conn)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 1000000

                    };

                    NpgsqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        category category = new category
                        {
                            cid = Convert.ToInt32(dr[0].ToString()),
                            cname = dr[1].ToString(),
                            created_by = dr[2].ToString(),
                            modified_by = dr[3].ToString(),
                        };
                        categories.Add(category);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return categories;
        }


        public Response products(Product body)
        {
            string status = "";
            Response response = new Response();
            decimal prod_id = 0;

            try
            {
                using (var dbContext = new ApplicationDbContext("public"))
                {
                    var Exists = dbContext.Products.Where(u => u.pname == body.pname).SingleOrDefault();

                    if (Exists == null)
                    {
                        products prod = new products
                        {
                            // uid == Unit_ID,
                            created_by = body.created_by,
                            pname = body.pname,
                            cid = Convert.ToInt32(body.cid),
                            uid = Convert.ToInt32(body.uid),
                            has_attr = body.has_attr
                            // Status = true,
                            //LastActive = DateTime.Now

                        };

                        dbContext.Products.Add(prod);
                        dbContext.SaveChanges();
                        prod_id = dbContext.Products.Where(u => u.pname == body.pname).SingleOrDefault().pid;
                    }
                    else
                    {
                        prod_id = Exists.pid;

                    }

                    if (prod_id != 0 && Exists == null)
                    {
                        status = "The product '" + body.pname + "' has been successfully added";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }else if (prod_id != 0 && Exists != null)
                    {
                        status = "The product '" + body.pname + "' has been successfully edited";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The product '" + body.pname + "' was not added. Please try again";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }
                }

               

            }
            catch (Exception Ex)
            {

            }
            // return userid;
            return response;

        }

        public Response EditProduct(Product body)
        {
            string status = "";
            Response response = new Response();
            decimal prod_id = 0;

            try
            {
                using (var dbContext = new ApplicationDbContext("public"))
                {
                    var Exists = dbContext.Products.Where(u => u.pid == body.pid).SingleOrDefault();

                    string oldName = Exists.pname;

                    if (Exists == null)
                    {
                       
                    }
                    else
                    {
                        prod_id = Exists.pid;

                        //products prod = new products
                        //{
                        //    // uid == Unit_ID,
                        //    pid = body.pid,
                        //    created_by = body.created_by,
                        //    pname = body.pname,
                        //    cid = Convert.ToInt32(body.cid),
                        //    uid = Convert.ToInt32(body.uid),
                        //    has_attr = body.has_attr
                        //    // Status = true,
                        //    //LastActive = DateTime.Now

                        //};

                        Exists.pid = body.pid;
                        Exists.modified_by = body.modified_by;
                        Exists.pname = body.pname;
                        Exists.cid = Convert.ToInt32(body.cid);
                        Exists.uid = Convert.ToInt32(body.uid);
                        Exists.has_attr = body.has_attr;

                        //dbContext.Products.Add(prod);
                        dbContext.SaveChanges();
                        string newPname = dbContext.Products.Where(u => u.pid == body.pid).SingleOrDefault().pname;
                    }

                    
                    if (prod_id != 0 && Exists != null)
                    {
                        status = "The product '" + oldName + "' has been successfully edited to '" +body.pname +"'";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The product '" + body.pname + "' was not found. Please try again";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }
                }



            }
            catch (Exception Ex)
            {

            }
            // return userid;
            return response;

        }
        public List<products> products(string cid)
        {
            List<products> Products = new List<products>();

            if (cid == null || cid == "0")
                cid = "";


            try
            {
                using (NpgsqlConnection conn = Common.GetConnection())
                {

                    //NULLIF(your_value, '')::int
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.vw_products WHERE (@cid = '' or cid = @cid::int)", conn)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 1000000

                    };
                    cmd.Parameters.Add("@cid", NpgsqlDbType.Text).Value = cid;

                    NpgsqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products product = new products
                        {
                            cid = Convert.ToInt32(dr[6].ToString()),
                            pid = Convert.ToInt32(dr[0].ToString()),
                            pname = dr[1].ToString(),
                            cname = dr[2].ToString(),
                            has_attr = Convert.ToBoolean(dr[3].ToString()),
                            created_by = dr[4].ToString(),//(emp != null) ? emp.Name : null
                            modified_by = dr[5].ToString(),
                            attr_id = Convert.ToInt32(dr[7].ToString()),
                            color = (dr[8].ToString() != null) ? dr[8].ToString() : "",
                            size = (dr[9].ToString() != null) ? dr[9].ToString() : "",
                            price = Convert.ToDecimal(dr[10].ToString()),
                        };
                        Products.Add(product);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return Products;
        }

        public Response productAttributes(Attributes body)
        {
            string status = "";
            Response response = new Response();
            decimal attr_id = 0;
            int pid = Convert.ToInt32(body.pid);

            try
            {
                using (var dbContext = new ApplicationDbContext("public"))
                {
                    var Exists = dbContext.Attributes.Where(u => u.pid == pid && u.color == body.color && u.size == body.size && u.gender == body.gender).SingleOrDefault();

                    if (Exists == null)
                    {
                        attributes attr = new attributes
                        {
                            // uid == Unit_ID,
                            pid = Convert.ToInt32(body.pid),
                            color = body.color,
                            size = body.size,
                            price = Convert.ToInt32(body.price),
                            created_by = body.created_by,
                            modified_by = body.modified_by,
                            gender = body.gender
                            //LastActive = DateTime.Now

                        };

                        dbContext.Attributes.Add(attr);
                        dbContext.SaveChanges();

                        attr_id = dbContext.Attributes.Where(u => u.pid == pid).OrderByDescending(x => x.attr_id).FirstOrDefault().attr_id;
                    }
                    else 
                    {
                        attr_id = Exists.attr_id;
                    }

                    if (attr_id != 0 && Exists == null)
                    {
                        status = "A new Attribute for product '" + body.pname + "' has been successfully added.  Attribute-Details " +
                            "( Color | " + body.color + " | Size | " + body.size + " | Price |" + body.price + " )";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else if (attr_id != 0 && Exists != null)
                    {
                        status = "The attribute for product '" + body.pname + "' already exists.  Attribute-Details " +
                            "( Color | " + Exists.color + " | Size | " + Exists.size + " | Price |" + Exists.price + " )";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The new Attribute for product '" + body.pname + "' was not added. Please try again";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }
                }

               

            }
            catch (Exception Ex)
            {

            }
            // return userid;
            return response;

        }

        public List<attributes> productAttributes()
        {
            List<attributes> Attributes = new List<attributes>();

            try
            {
                using (NpgsqlConnection conn = Common.GetConnection())
                {

                    //NULLIF(your_value, '')::int
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.vw_products", conn)// WHERE has_attr = 1
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 1000000

                    };

                    NpgsqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        attributes attr = new attributes
                        {
                            //cid = Convert.ToInt32(dr[6].ToString()),
                            
                            pid = Convert.ToInt32(dr[0].ToString()),
                            pname = dr[1].ToString(),
                            //cname = dr[2].ToString(),
                            //has_attr = Convert.ToBoolean(dr[3].ToString()),
                            created_by = dr[4].ToString(),
                            modified_by = dr[5].ToString(),
                            attr_id = Convert.ToInt32(dr[7].ToString()),
                            color = dr[8].ToString(),
                            size = dr[9].ToString(),
                            price = Convert.ToDecimal(dr[10].ToString()),
                        };
                        Attributes.Add(attr);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return Attributes;
        }

        public List<attributes> productAttributeValues(string attr_id)
        {
            List<attributes> Attributes = new List<attributes>();

            try
            {
                using (NpgsqlConnection conn = Common.GetConnection())
                {

                    //NULLIF(your_value, '')::int
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.vw_products WHERE attr_id = @attr_id ::int", conn)// WHERE has_attr = 1
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 1000000

                    };
                    cmd.Parameters.Add("@attr_id", NpgsqlDbType.Text).Value = attr_id;

                    NpgsqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        attributes attr = new attributes
                        {
                            //cid = Convert.ToInt32(dr[6].ToString()),

                            pid = Convert.ToInt32(dr[0].ToString()),
                            pname = dr[1].ToString(),
                            //cname = dr[2].ToString(),
                            //has_attr = Convert.ToBoolean(dr[3].ToString()),
                            created_by =  dr[4].ToString(),
                            modified_by = dr[5].ToString(),
                            attr_id = Convert.ToInt32(dr[7].ToString()),
                            color = dr[8].ToString(),
                            size = dr[9].ToString(),
                            price = Convert.ToDecimal(dr[10].ToString()),
                        };
                        Attributes.Add(attr);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            return Attributes;
        }

        public Response UpdateproductAttributes(Attributes body)
        {
            string status = "";
            Response response = new Response();
            decimal attr_id = 0;
            int pid = Convert.ToInt32(body.pid);

            try
            {
                using (var dbContext = new ApplicationDbContext("public"))
                {
                    var Exists = dbContext.Attributes.Where(u => u.attr_id ==  body.attr_id).SingleOrDefault();

                    if (Exists == null)
                    {
                        
                    }
                    else
                    {
                        attr_id = Exists.attr_id;

                        Exists.color = body.color;
                        Exists.size = body.size;
                        Exists.gender = body.gender;
                        Exists.price = body.price;

                        dbContext.SaveChanges();
                    }

                       
                    if (attr_id != 0 && Exists != null)
                    {
                        status = "The attribute for product '" + Exists.pname + "' has been successfully updated.  Attribute-Details " +
                            "( Color | " + body.color + " | Size | " + body.size + " | Price |" + body.price + " | Gender |" + body.gender + " )";
                        response.ResponseCode = 1;
                        response.ResponseDetails = status;

                    }
                    else
                    {
                        status = "The new Attribute for product '" + body.pname + "' was not added. Please try again";
                        response.ResponseCode = 2;
                        response.ResponseDetails = status;
                    }
                }



            }
            catch (Exception Ex)
            {

            }
            // return userid;
            return response;
        }
        }
}
