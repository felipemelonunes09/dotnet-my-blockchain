using System;

namespace my_blockchain.blockchain.core
{

    class Header { 
        public long nonce { get; private set; }
        public string hash { get; private set; }

        public Header(long nonce, string hash) {
            this.nonce = nonce;
            this.hash = hash;
        }

    }

    class Payload {
        public long sequence { get; private set; } = 0;
        public long timestamp { get; private set; } = 0 ;
        public object data { get; private set; }
        public string lastHash { get; private set; }

        public Payload ( long sequence, object data, string lastHash ) {
            this.sequence = sequence;
            this.data = data;
            this.lastHash = lastHash;
            this.timestamp = Utils.GetTimestamp();
        }

        public override string ToString()
        {

            return "Payload { \n" +
                    "   sequence: " + this.sequence + "\n" +
                    "   data: " + this.data.ToString() + "\n" +
                    "   lastHash: " + this.lastHash + "\n" +
                    "   timestamp: " + this.timestamp + "\n" +
                    "} ";
        }
    }

    class Block {
        
        public Header header { get; private set; }
        public Payload payload { get; private set; }
        public Block(Header header, Payload payload) {
            this.header = header;
            this.payload = payload;
        }

        public override string ToString()
        {
            return "  -Block #" + this.payload.sequence +
                    " -Nonce: " + this.header.nonce +
                    " -Hash: " + this.header.hash; 
        }
    }
}