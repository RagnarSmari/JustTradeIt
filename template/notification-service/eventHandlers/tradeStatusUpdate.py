def setup_handler(channel, exchange_name):
    # TODO: Implement
    connection = pika.BlockingConnection(  
        pika.ConnectionParameters(host="localhost"))
    channel = connection.channel()
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)


    result = channel.queue_declare(queue='trade-update-request', exclusive=True)
    queue_name = result.method.queue