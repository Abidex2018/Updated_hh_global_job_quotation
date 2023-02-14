HH_Global_Job_Quotation

Endpoint for the docker container: http://localhost:8080/api/CreateInvoice
Endpoint for the kubernetes: http://localhost:80/api/CreateInvoice

Inputs to run the endpoint using the provided example in the document provided

{ "isExtraMargin": true, "items": [ { "itemName": "envelopes", "price": 19385.38, "isExempt": true }, { "itemName": "letterhead", "price": 1829, "isExempt": true } ] }

Also the documentation of the Request Parameters and response also exist in the context of the Webapi service of the swagger documentation.

I hard an some error while running the docker, which I wish I can fix but the recommendation in resolving it did not work for me.
