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
using System.Collections.Generic;

namespace Classless.Encoder {
	/// <summary>An implementation of the Base64 decoding algorithm.</summary>
	public class Base64Decoder : BlockDecoder {
		private List<byte> resultBuffer = new List<byte>();
		private char[] alphabet;
		private char padding;


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

		/// <summary>Gets or sets the padding character used for encoding.</summary>
		public char Padding {
			get { return padding; }
			set { padding = value; }
		}


		/// <summary>Initializes a new instance of the Base64Decoder class.</summary>
		public Base64Decoder() : this(Base64Encoder.StandardAlphabet, Base64Encoder.StandardPadding) { }

		/// <summary>Initializes a new instance of the Base64Decoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		public Base64Decoder(char[] alphabet) : this(alphabet, Base64Encoder.StandardPadding) { }

		/// <summary>Initializes a new instance of the Base64Decoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		/// <param name="padding">The character to use when padding the resultBuffer.</param>
		public Base64Decoder(char[] alphabet, char padding) : base(4) {
			Alphabet = alphabet;
			Padding = padding;
		}


		/// <summary>Gets a value indicating whether the decoding is case sensitive.</summary>
		override public bool IsCaseSensitive {
			get { return true; }
		}


		/// <summary>Returns the Encoder that corresponds with this Decoder.</summary>
		/// <returns>A new instance of the corresponding Encoder class.</returns>
		override public Encoder GetEncoder() {
			return new Base64Encoder();
		}


		/// <summary>Returns the expected Encoding of the strings that will be decoded.</summary>
		/// <returns>An instance of the corresponding Encoding class.</returns>
		override public System.Text.Encoding GetInputEncoding() {
			return System.Text.Encoding.ASCII;
		}


		/// <summary>Initializes the Decoder in preparation for a decoding operation.</summary>
		override public void Initialize() {
			base.Initialize();
			resultBuffer = new List<byte>();
		}


		/// <summary>Process a block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		override protected void ProcessBlock(char[] array, int offset) {
			byte[] temp = new byte[4];

			for (int i = 0; i < 4; i++) {
				if (array[offset + i] == padding) {
					ProcessFinalBlock(array, offset, i);
					return;
				}
				temp[i] = TranslateCharacter(array[offset + i]);
			}

			resultBuffer.Add((byte)((temp[0] << 2) | (temp[1] >> 4)));
			resultBuffer.Add((byte)((temp[1] << 4) | (temp[2] >> 2)));
			resultBuffer.Add((byte)((temp[2] << 6) | temp[3]));
		}


		/// <summary>Process the last block of data.</summary>
		/// <param name="array">The block of data to process.</param>
		/// <param name="offset">Where to start in the block.</param>
		/// <param name="length">How many bytes need to be processed.</param>
		/// <returns>The results of the completed decoding operation.</returns>
		override protected byte[] ProcessFinalBlock(char[] array, int offset, int length) {
			byte[] temp = new byte[4];

			for (int i = 0; i < length; i++) {
				temp[i] = TranslateCharacter(array[offset + i]);
			}

			switch (length) {
				case 2:
					resultBuffer.Add((byte)((temp[0] << 2) | (temp[1] >> 4)));
					break;

				case 3:
					resultBuffer.Add((byte)((temp[0] << 2) | (temp[1] >> 4)));
					resultBuffer.Add((byte)((temp[1] << 4) | (temp[2] >> 2)));
					break;

				case 4:
					resultBuffer.Add((byte)((temp[0] << 2) | (temp[1] >> 4)));
					resultBuffer.Add((byte)((temp[1] << 4) | (temp[2] >> 2)));
					resultBuffer.Add((byte)((temp[2] << 6) | temp[3]));
					break;
			}

			return resultBuffer.ToArray();
		}


		/// <summary>Determines the byte value for the specified character.</summary>
		/// <param name="input">The character to translate.</param>
		/// <returns>The byte value for the character in the defined Alphabet.</returns>
		/// <exception cref="ArgumentException">When the character is note defined in the current Alphabet.</exception>
		virtual protected byte TranslateCharacter(char input) {
			int idx = Array.IndexOf(alphabet, input);
			if (idx < 0) {
				throw new ArgumentException(Properties.Resources.illegalCharacter, "input");
			}
			return (byte)idx;
		}
	}
}
