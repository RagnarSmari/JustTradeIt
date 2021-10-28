# JustTradeIt


Token: 
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

AccessKeyId: AKIA5STCBBGXEOPYZXD3 
Secret Access Key: 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 

"Aws": {
    "BucketName": "raggistradebucket",
    "KeyId": "AAKIA5STCBBGXEOPYZXD3",
    "KeySecret": "A3zCqegXBtHKnQ2Nww9Tssj6vR9+kbYO9tGCSeLB"
  }


TODO overall:

Finish the Notification service
Check where to upload images to aws bucket and in what routes
Finish the aws bucket
Test all the routes
Test with the PostmanScript

Little todo:
Global exception handler
Upload images to aws s3 bucket when updateing a profile




ROUTES:



AccountController:

POST Register -> done -> tested
POST login -> done -> tested
GET logout -> done -> tested
GET profile -> done -> tested
PUT profile -> not implemented
	Request not working as postman says unsupported media type
	

TradeController:
GET trades -> done
POST trades -> done
GET trade/id -> done
PUT trade/id -> done

ItemController:
GET items -> done
GET items/id -> done
POST items -> done
DELETE items/id -> done

UserController
GET users/id -> done
GET users/id/trades -> done



RabbitMQ:
This service works as a glue between the other services to communicate with each other via AMQP

