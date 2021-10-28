import pika 
import sys



def setup_handler(channel, exchange_name):
    # TODO: Implement
    connection = pika.BlockingConnection(  
        pika.ConnectionParameters(host="localhost"))
    channel = connection.channel()

    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

    result = channel.queue_declare(queue='new-trade-queue', exclusive=True)
    queue_name = result.method.queue
    

    channel.queue_bind(exchange=exchange_name, queue=queue_name)

    print(' [*] Waiting for logs. To exit press CTRL+C')
    
    def callback(ch, method, properties, body):
        print(" [x] %r" % body)

    channel.basic_consume(
        queue=queue_name, on_message_callback=callback, auto_ack=True)

    channel.start_consuming()
