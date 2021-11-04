import pika
import json
import sys
from services import emailservice

def setup_handler(channel, exchange_name):
    
    exchange_name = 'trade-exchange'
    create_order_routing_key = 'new-trade-request'
    email_queue_name = 'new-trade-queue'

    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=email_queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)
    print(f"Consuming Queue: {email_queue_name}")
    
    def callback(ch, method, properties, body):
        # Call the email service
        # Body is then the email in json, parse it
        emailservice.send_newOrder_email(ch, method, properties, body)

        


    channel.basic_consume(
                    email_queue_name,
                    on_message_callback=callback,
                    auto_ack=True
                    )