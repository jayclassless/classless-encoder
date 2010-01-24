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
using System.Text;

namespace Classless.Encoder {
	/// <summary>An implementation of the Unix-to-Unix encoding algorithm.</summary>
	public class UUEncoder : BlockEncoder {
		private StringBuilder resultBuffer = new StringBuilder();
		private char[] alphabet;


		/// <summary>The standard alphabet for Unix-to-Unix encoding.</summary>
		static public readonly char[] StandardAlphabet = new char[64] {
			'`', '!', '"', '#', '$', '%', '&', '\'',
			'(', ')', '*', '+', ',', '-', '.', '/',
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', ':', ';', '<', '=', '>', '?',
			'@', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
			'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
			'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
			'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
		};


		/// <summary>Gets or sets the alphabet used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 64 characters.</exception>
		public char[] Alphabet {
			get { return alphabet; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 64) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				alphabet = value;
			}
		}


		/// <summary>Initializes a new instance of the UUEncoder class.</summary>
		public UUEncoder() : this(StandardAlphabet) { }

		/// <summary>Initializes a new instance of the UUEncoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		public UUEncoder(char[] alphabet) : base(45) {
			Alphabet = alphabet;
		}


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			//return new UUDecoder();
			throw new NotImplementedException(Properties.Resources.decoderNotImplemented);
		}


		/// <summary>Returns the Encoding of the result strings.</summary>
		/// <returns>An instance of the corresponding Encoding class.</returns>
		override public Encoding GetOutputEncoding() {
			return Encoding.ASCII;
		}


		/// <summary>Initializes the Encoder in preparation for an encoding operation.</summary>
		override public void Initialize() {
			base.Initialize();
			resultBuffer = new StringBuilder();
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		override protected void ProcessBlock(byte[] array, int offset) {
			resultBuffer.Append(alphabet[BlockSize & 0x3F]);
			for (int i = 0; i < (BlockSize / 3); i++) {
				Encode(array, offset + (i * 3));
			}
			resultBuffer.Append('\n');
		}
		

		/// <summary>Process the last block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		/// <param name="length">How many bytes need to be processed.</param>
		/// <returns>The results of the completed encoding operation.</returns>
		override protected string ProcessFinalBlock(byte[] array, int offset, int length) {
			resultBuffer.Append(alphabet[length & 0x3F]);
			for (int i = 0; i < (length / 3); i++) {
				Encode(array, offset + (i * 3));
			}
			int leftover = length % 3;
			if (leftover > 0) {
				byte[] temp = new byte[3];
				if (leftover == 1) {
					temp[0] = array[length - 1];
				} else {
					temp[0] = array[length - 2];
					temp[1] = array[length - 1];
				}
				Encode(temp, 0);
			}
			resultBuffer.Append('\n');

			if (resultBuffer.Length > 2) {
				resultBuffer.Append(alphabet[0]).Append('\n');
			}

			return resultBuffer.ToString();
		}


		/// <summary>Encodes a set of 3 bytes.</summary>
		/// <param name="array">The array to encode.</param>
		/// <param name="offset">Where to start in the array.</param>
		protected void Encode(byte[] array, int offset) {
			resultBuffer.Append(alphabet[(array[offset] >> 2) & 0x3F]);
			resultBuffer.Append(alphabet[((array[offset] << 4) & 0x30) | ((array[offset + 1] >> 4) & 0x0F)]);
			resultBuffer.Append(alphabet[((array[offset + 1] << 2) & 0x3C) | ((array[offset + 2] >> 6) & 0x03)]);
			resultBuffer.Append(alphabet[array[offset + 2] & 0x3F]);
		}
	}
}
