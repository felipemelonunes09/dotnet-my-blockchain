using System;
using System.Collections.Generic;
using System.Linq;

namespace my_blockchain.blockchain.core
{

    class Blockchain {

        public List<Block> chain { get; private set; }
        public char prefixPow { get; private set; } = '0';
        public int level { get; private set; } = 4;

        public Blockchain(int level) {

            this.level = level;

            chain = new List<Block>();
            chain.Add(this.Genesys());
        }

        private Block Genesys() { 

            Payload payload = new Payload(
                sequence: 0,
                data: "Initial Block",
                lastHash: ""
            );

            return new Block(
                new Header(
                    nonce: 0,
                    hash: Hash.GenHash( Utils.PayloadToJsonString( payload ) )
                ),
                payload
            );
        }

        public Payload CreateBlock(object data) {
            Payload newBlock = new Payload(
                sequence: this.GetLastBlock().payload.sequence + 1,
                lastHash: this.GetLastHashBlock(),
                data: data
            );

            Console.WriteLine("Block #" + newBlock.sequence + " created " + newBlock.ToString());
            return newBlock;
        }

        public Block Minerate(Payload payload) { 

            long nonce = 0;
            long start = Utils.GetTimestamp();

            while(true) {

                string hash = Hash.GenHash( Utils.PayloadToJsonString( payload ) );
                string hashPow = Hash.GenHash( hash + nonce );

                if (Hash.validateHash(new Hash.ValidatePack(){ hash = hashPow, level = this.level, prefix = this.prefixPow } )) {

                    long finish = Utils.GetTimestamp();
                    string reduce = hash.Substring(1, 12);
                    long hashTime = (finish - start);

                    Console.WriteLine("Block #" + payload.sequence + " minerated on " + hashTime + "s Hash: " + reduce + " (nonce "+ nonce +") ");
                    return new Block(
                        new Header(nonce, hash),
                        payload
                    );
                }
                nonce++;
            }
        }

        public List<Block> SendBlock( Block block ) {
            
            if ( this.ValidateBlock(block) ) {
                this.chain.Add(block);
                Console.WriteLine(block.ToString() + " on chain ");
            }

            return this.chain;
        }

        private bool ValidateBlock(Block block) {

            if (block.payload.lastHash != this.GetLastHashBlock()) {
                Console.WriteLine(block.ToString() + " Could not be validated");
                return false;
            }

            string hashTeste = Hash.GenHash( Hash.GenHash( Utils.PayloadToJsonString( block.payload ) ) + block.header.nonce );
            if (!Hash.validateHash(new Hash.ValidatePack() { hash = hashTeste, level = this.level, prefix = this.prefixPow} )) {
                Console.WriteLine(block.ToString() + " is not valid");
                return false;
            }   

            return true;
        }

        private Block GetLastBlock() { 
            return this.chain.Last();
        }

        private string GetLastHashBlock() {
            return this.GetLastBlock().header.hash;
        }
    }
}