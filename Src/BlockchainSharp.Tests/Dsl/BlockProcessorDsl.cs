﻿namespace BlockchainSharp.Tests.Dsl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BlockchainSharp.Core;

    public class BlockProcessorDsl
    {
        private static char[] separators = new char[] { ' ', '\t' };
        private BlockProcessor processor;
        private IDictionary<string, Block> blocks = new Dictionary<string, Block>();

        public BlockProcessorDsl(BlockProcessor processor)
        {
            this.processor = processor;
            Block genesis = new Block(0, null);
            this.processor.Process(genesis);
        }

        public void Run(string[] commands)
        {
            foreach (var command in commands)
                this.Run(command);
        }

        public void Run(string command)
        {
            if (command == null)
                return;

            int p = command.IndexOf('#');

            if (p >= 0)
                command = command.Substring(0, p);

            command = command.Trim();

            if (command.Length == 0)
                return;

            var words = command.Split(separators);

            if (words[0] == "chain")
                this.RunChain(words.Skip(1));
            else if (words[0] == "send")
                this.RunSend(words.Skip(1));
            else if (words[0] == "top")
                this.RunTop(words[1]);
            else
                throw new InvalidOperationException(string.Format("Unknow action {0}", words[0]));
        }

        private void RunChain(IEnumerable<string> bnames)
        {
        }

        private void RunSend(IEnumerable<string> bnames)
        {
        }

        private void RunTop(string bname)
        {
            Block block = this.processor.BlockChain.BestBlock;
            Block b = this.blocks[bname];

            if (b.Number != block.Number || !b.Hash.Equals(block.Hash))
                throw new InvalidOperationException(string.Format("Unexpected best block {0}", block.Number));
        }
    }
}
