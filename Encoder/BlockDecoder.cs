#region License
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Classless.Encoder - .NET Binary Encoding Library.
 *
 * The Initial Developer of the Original Code is Classless.net.
 * Portions created by the Initial Developer are Copyright (C) 2010 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;

namespace Classless.Encoder {
	/// <summary>An abstract implementation of a Decoder that processes data in defined blocks of bytes.</summary>
	abstract public class BlockDecoder : Decoder {
		private int blockSize;
		private char[] buffer;
		private int bufferCount;


		/// <summary>The size in bytes of an individual block.</summary>
		public int BlockSize {
			get { return blockSize; }
		}

		/// <summary>The number of bytes currently in the buffer waiting to be processed.</summary>
		public int BufferCount {
			get { return bufferCount; }
		}


		/// <summary>Initializes a new instance of the BlockDecoder class.</summary>
		/// <param name="blockSize">The size in bytes of an individual block.</param>
		public BlockDecoder(int blockSize) {
			if (blockSize < 1) {
				throw new ArgumentException(Properties.Resources.invalidBlockSize, "blockSize");
			}
			this.blockSize = blockSize;
			buffer = new char[blockSize];
		}


		/// <summary>Initializes the Decoder in preparation for an encoding operation.</summary>
		override public void Initialize() {
			buffer = new char[blockSize];
			bufferCount = 0;
		}
		

		/// <summary>Decodes an array of characters.</summary>
		/// <param name="array">The array of characters to decode.</param>
		/// <param name="offset">The index in the array to start decoding from.</param>
		/// <param name="length">The number of characters in the array to decode.</param>
		override public void DecodeCore(char[] array, int offset, int length) {
			int i;

			// Use what may already be in the buffer.
			if (bufferCount > 0) {
				if (length < (blockSize - bufferCount)) {
					// Still don't have enough for a full block, just store it.
					Array.Copy(array, offset, buffer, bufferCount, length);
					bufferCount += length;
					return;
				} else {
					// Fill out the buffer to make a full block, and then process it.
					i = blockSize - bufferCount;
					Array.Copy(array, offset, buffer, bufferCount, i);
					ProcessBlock(buffer, 0);
					bufferCount = 0;
					offset += i;
					length -= i;
				}
			}

			// For as long as we have full blocks, process them.
			for (i = 0; i < (length - (length % blockSize)); i += blockSize) {
				ProcessBlock(array, offset + i);
			}

			// If we still have some bytes left, store them for later.
			int bytesLeft = length % blockSize;
			if (bytesLeft != 0) {
				Array.Copy(array, ((length - bytesLeft) + offset), buffer, 0, bytesLeft);
				bufferCount = bytesLeft;
			}
		}


		/// <summary>Completes the decoding operation and returns the result.</summary>
		/// <returns>The result of the decoding operation.</returns>
		override public byte[] DecodeFinal() {
			return ProcessFinalBlock(buffer, 0, bufferCount);
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		abstract protected void ProcessBlock(char[] array, int offset);


		/// <summary>Process the last block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		/// <param name="length">How many bytes need to be processed.</param>
		/// <returns>The results of the completed decoding operation.</returns>
		abstract protected byte[] ProcessFinalBlock(char[] array, int offset, int length);
	}
}
