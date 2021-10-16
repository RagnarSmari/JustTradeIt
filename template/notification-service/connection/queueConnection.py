import pika
import os

exchange_name = '<name-of-exchange>'

def setup():
    queue_host = os.environ.get('QUEUE_HOST') or 'localhost'
    connection = pika.BlockingConnection(pika.ConnectionParameters(queue_host, credentials=pika.PlainCredentials('<username>', '<password>')))
    channel = connection.channel()
    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

    return (
        channel,
        connection,
        exchange_name
    )