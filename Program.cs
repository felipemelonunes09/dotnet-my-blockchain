using System;
using my_blockchain.blockchain.core;

namespace my_blockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MINING_LEVEL = 4;
            const int MINING_BLOCKS = 100;

            Blockchain mychain = new Blockchain(MINING_LEVEL);
            
            Console.WriteLine(" ____ BLOCK NUMBERS _____ ");
            Console.WriteLine(MINING_BLOCKS);

            for(int i = 1; i < MINING_BLOCKS; i++) {
                
                Payload payload = mychain.CreateBlock("Block: " + i);
                Block block = mychain.Minerate(payload);

                var chain = mychain.SendBlock(block);
                Console.WriteLine(block.header.hash);
            }

            Console.WriteLine(" _____ BlockChain _____ ");
            Console.WriteLine(mychain.chain.Count +  "# Blocks on chain");
        }
    }
}
