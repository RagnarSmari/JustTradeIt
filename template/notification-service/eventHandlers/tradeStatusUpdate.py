import pika
from services import emailservice

def setup_handler(channel, exchange_name):
    connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()
    exchange_name = 'trade-exchange'

    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

    result = channel.queue_declare(queue='trade-update-queue', durable=True)

    queue_name = result.method.queue

    channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key='trade-update-request')

    print(' [*] Waiting for logs. To exit press CTRL+C')

    def callback(ch, method, properties, body):
        emailservice.send_update_email(ch, method, properties, body)
        #print(" [x] %r" % body)

    channel.basic_consume(
        queue=queue_name, on_message_callback=callback, auto_ack=True)

    channel.start_consuming()