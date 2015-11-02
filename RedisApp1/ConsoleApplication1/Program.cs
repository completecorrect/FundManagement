using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisApp1
{
    class Program
    {
        //private const string RedisConStr = "192.168.99.100:12345";
        private const string RedisConStr = "127.0.0.1:6379";

        /* 
        /// <summary>
        /// http://geekswithblogs.net/shaunxu/archive/2012/04/27/redis-on-windows.aspx
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            using (var redis = new RedisClient(RedisConStr))
            {
                redis.FlushAll();
            }
            

            //Dequeue();
            //Enqueue();
        }
        */

        static void Main(string[] args)
        {
            using (var redis = new RedisClient(RedisConStr))
            {
                redis.FlushAll();
            }

            var consumerThread1 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            var consumerThread2 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            var consumerThread3 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            consumerThread1.Start("Consumer 1");
            consumerThread2.Start("Consumer 2");
            consumerThread3.Start("Consumer 3");

            using (var publisher = new RedisClient(RedisConStr))
            {
                Console.WriteLine("Input message: ");
                var message = Console.ReadLine();
                while (!string.IsNullOrWhiteSpace(message))
                {
                    publisher.PublishMessage("default", message);
                    Console.WriteLine("Input message: ");
                    message = Console.ReadLine();
                }
            }


            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void ConsumerAction(object name)
        {
            using (var consumer = new RedisClient(RedisConStr))
            {
                using (var subscription = consumer.CreateSubscription())
                {
                    subscription.OnSubscribe = (channel) =>
                    {
                        Console.WriteLine("[{0}] Subscribe to channel '{1}'.", name, channel);
                    };
                    subscription.OnUnSubscribe = (channel) =>
                    {
                        Console.WriteLine("[{0}] Unsubscribe to channel '{1}'.", name, channel);
                    };
                    subscription.OnMessage = (channel, message) =>
                    {
                        Console.WriteLine("[{0}] Received message '{1}' from channel '{2}'.", name, message, channel);
                    };

                    subscription.SubscribeToChannels("default");
                }
            }
        }
        
        private static void Dequeue()
        {
            using (var redis = new RedisClient(RedisConStr))
            {
                var result = string.Empty;
                while (string.Compare("q", result, false) != 0)
                {
                    result = redis.BlockingDequeueItemFromList("default", TimeSpan.FromSeconds(5));
                    Console.WriteLine("DEQUEUE RESULT = [{0}]", result);
                }
            }
        }

        private static void Enqueue()
        {
            var senderThread = new Thread(() =>
            {
                using (var redis = new RedisClient(RedisConStr))
                {
                    Console.WriteLine("Input message: ");
                    var message = Console.ReadLine();
                    while (!string.IsNullOrWhiteSpace(message))
                    {
                        redis.EnqueueItemOnList("default", message);

                        Console.WriteLine("Input message: ");
                        message = Console.ReadLine();
                    }
                }
            });
            senderThread.Start();
        }
        
        /*
        static void Main(string[] args)
        {
            var messagesReceived = 0;

            using (var redisConsumer = new RedisClient("127.0.0.1"))
            using (var subscription = redisConsumer.CreateSubscription())
            {
                subscription.OnSubscribe = channel =>
                {
                    Console.WriteLine("Subscribed to '{0}'", channel);
                };
                subscription.OnUnSubscribe = channel =>
                {
                    Console.WriteLine("UnSubscribed from '{0}'", channel);
                };
                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine("Received '{0}' from channel '{1}'", msg, channel);

                    //As soon as we've received all 5 messages, disconnect by unsubscribing to all channels
                    if (++messagesReceived == PublishMessageCount)
                    {
                        subscription.UnSubscribeFromAllChannels();
                    }
                };

                ThreadPool.QueueUserWorkItem(x =>
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Begin publishing messages...");

                    using (var redisPublisher = new RedisClient(TestConfig.SingleHost))
                    {
                        for (var i = 1; i <= PublishMessageCount; i++)
                        {
                            var message = MessagePrefix + i;
                            Console.WriteLine("Publishing '{0}' to '{1}'", message, ChannelName);
                            redisPublisher.PublishMessage(ChannelName, message);
                        }
                    }
                });

                Console.WriteLine("Started Listening On '{0}'", ChannelName);
                subscription.SubscribeToChannels(ChannelName); //blocking
            }

            Console.WriteLine("EOF");
        }
        */
    }
}
