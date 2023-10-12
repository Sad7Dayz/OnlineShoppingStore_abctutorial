using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
			APIContext apiContext = PaypalConfiguration.GetAPIContext();
			try
			{

				string payerId = Request.Params["PayerID"];
				if (string.IsNullOrEmpty(payerId))
				{
					string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPapal?";

					var Guid = Convert.ToString((new Random()).Next(100000));
					var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + Guid);

					var links = createdPayment.links.GetEnumerator();
					string paypalRedirectUrl = null;

					while (links.MoveNext())
					{
						Links lnk = links.Current;

						if (lnk.rel.ToLower().Trim().Equals("approval_url"))
						{
                            paypalRedirectUrl = lnk.href;
						}
					}

                    Session.Add(Guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
				else
				{
					var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);


                    if (executedPayment.state.ToLower() != "approved")
					{
						return View("FailureView");
					}

				}
			}
			catch (Exception e)
			{
                string em = e.Message;
				return View("FailureView");
				//throw;
			}
			return View("SuccessView");
		}

		private PayPal.Api.Payment payment;
		
        private Payment ExecutePayment(APIContext apicontext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution() { payer_id = payerId };
			this.payment = new Payment() { id = paymentId };
			return this.payment.Execute(apicontext, paymentExecution);
		}


		private Payment CreatePayment(APIContext apicontext, string redirectUrl)
		{
            var ItemLIst = new ItemList() { items = new List<Item>() };

            if (Session["cart"] != null)
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);
                foreach (var item in cart)
                {
                    ItemLIst.items.Add(new Item()
                    {
                        name = item.Product.ProductName.ToString(),
                        currency = "USD",
                        price = item.Product.Price.ToString(),
                        quantity = item.Product.Quantity.ToString(),
                        sku = "sku"
                    });


                }


                var payer = new Payer() { payment_method = "paypal" };

                var redirUrls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };

                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = Session["SesTotal"].ToString(),
                    details = details
                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = amount,
                    item_list = ItemLIst

                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrls
                };
            }

            return this.payment.Create(apicontext);
		
		}
	}
}