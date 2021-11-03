import pika
import json
import sys
from services import emailservice
import requests

def setup_handler(channel, exchange_name):
    connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
    channel = connection.channel()
    exchange_name = 'trade-exchange'
    create_order_routing_key = 'new-trade-request'
    email_queue_name = 'new_trade_queue'
    email_template = '<h2>Thank you for ordering @ Cactus heaven!</h2><p>We hope you will enjoy our lovely product and dont hesitate to contact us if there are any questions.</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Description</th><th>Unit price</th><th>Quantity</th><th>Row price</th></tr></thead><tbody>%s</tbody></table>'

    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=email_queue_name, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)


    print(' [*] Waiting for logs. To exit press CTRL+C')

    def callback(ch, method, properties, body):
        # Call the email service
        # Body is then the email in json, parse it
        print(" [x] %r" % body)
        emailservice.send_newOrder_email(ch, method, properties, body)

        


    channel.basic_consume(
                    email_queue_name,
                    callback,
                    auto_ack=True
                    )



    channel.start_consuming()
    connection.close()