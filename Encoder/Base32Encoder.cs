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
 * The Original Code is Classless.Hasher - .NET Binary Encoding Library.
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
	/// <summary>An implementation of the Base32 encoding algorithm.</summary>
	public class Base32Encoder : BlockEncoder {
		private StringBuilder resultBuffer = new StringBuilder();
		private char[] alphabet;
		private char padding;


		/// <summary>The standard alphabet defined by RFC4648.</summary>
		static public readonly char[] StandardAlphabet = new char[32] {
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
			'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
			'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
			'Y', 'Z', '2', '3', '4', '5', '6', '7',
		};

		/// <summary>The standard padding character defined by RFC4648.</summary>
		static public readonly char StandardPadding = '=';


		/// <summary>Gets or sets the alphabet used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 32 characters.</exception>
		public char[] Alphabet {
			get { return alphabet; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 32) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				alphabet = value;
			}
		}

		/// <summary>Gets or sets the padding character used for encoding.</summary>
		public char Padding {
			get { return padding; }
			set { padding = value; }
		}


		/// <summary>Initializes a new instance of the Base32Encoder class.</summary>
		public Base32Encoder() : this(StandardAlphabet, StandardPadding) { }

		/// <summary>Initializes a new instance of the Base32Encoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		public Base32Encoder(char[] alphabet) : this(alphabet, StandardPadding) { }

		/// <summary>Initializes a new instance of the Base32Encoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		/// <param name="padding">The character to use when padding the resultBuffer.</param>
		public Base32Encoder(char[] alphabet, char padding) : base(5) {
			Alphabet = alphabet;
			Padding = padding;
		}


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			return new Base32Decoder();
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
			resultBuffer.Append(alphabet[(array[offset] >> 3) & 0x1F]);
			resultBuffer.Append(alphabet[((array[offset] << 2) | (array[offset + 1] >> 6)) & 0x1F]);
			resultBuffer.Append(alphabet[(array[offset + 1] >> 1) & 0x1F]);
			resultBuffer.Append(alphabet[((array[offset + 1] << 4) | (array[offset + 2] >> 4)) & 0x1F]);
			resultBuffer.Append(alphabet[((array[offset + 2] << 1) | (array[offset + 3] >> 7)) & 0x1F]);
			resultBuffer.Append(alphabet[(array[offset + 3] >> 2) & 0x1F]);
			resultBuffer.Append(alphabet[((array[offset + 3] << 3) | (array[offset + 4] >> 5)) & 0x1F]);
			resultBuffer.Append(alphabet[array[offset + 4] & 0x1F]);
		}
		

		/// <summary>Process the last block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		/// <param name="length">How many bytes need to be processed.</param>
		/// <returns>The results of the completed encoding operation.</returns>
		override protected string ProcessFinalBlock(byte[] array, int offset, int length) {
			switch (length) {
				case 4:
					resultBuffer.Append(alphabet[(array[offset] >> 3) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset] << 2) | (array[offset + 1] >> 6)) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 1] >> 1) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset + 1] << 4) | (array[offset + 2] >> 4)) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset + 2] << 1) | (array[offset + 3] >> 7)) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 3] >> 2) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 3] << 3) & 0x1F]);
					resultBuffer.Append(padding);
					break;

				case 3:
					resultBuffer.Append(alphabet[(array[offset] >> 3) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset] << 2) | (array[offset + 1] >> 6)) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 1] >> 1) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset + 1] << 4) | (array[offset + 2] >> 4)) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 2] << 1) & 0x1F]);
					resultBuffer.Append(padding, 3);
					break;

				case 2:
					resultBuffer.Append(alphabet[(array[offset] >> 3) & 0x1F]);
					resultBuffer.Append(alphabet[((array[offset] << 2) | (array[offset + 1] >> 6)) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 1] >> 1) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset + 1] << 4) & 0x1F]);
					resultBuffer.Append(padding, 4);
					break;

				case 1:
					resultBuffer.Append(alphabet[(array[offset] >> 3) & 0x1F]);
					resultBuffer.Append(alphabet[(array[offset] << 2) & 0x1F]);
					resultBuffer.Append(padding, 6);
					break;
			}

			return resultBuffer.ToString();
		}
	}
}
