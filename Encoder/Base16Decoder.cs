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
using System.Collections.Generic;

namespace Classless.Encoder {
	/// <summary>An implementation of the Base16 decoding algorithm.</summary>
	public class Base16Decoder : Decoder {
		private const int NoNibbleCached = -1;

		private List<byte> resultBuffer = new List<byte>();
		private int lastNibble = NoNibbleCached;
		private char[] alphabet;


		/// <summary>Gets or sets the alphabet used for encoding.</summary>
		/// <exception cref="ArgumentNullException">If the specified value is null.</exception>
		/// <exception cref="ArgumentException">If the specified array contains less than 16 characters.</exception>
		public char[] Alphabet {
			get { return alphabet; }
			set {
				if (value == null) {
					throw new ArgumentNullException("value", Properties.Resources.alphabetCantBeNull);
				}
				if (value.Length < 16) {
					throw new ArgumentException(Properties.Resources.alphabetTooShort, "value");
				}
				alphabet = value;
			}
		}


		/// <summary>Initializes a new instance of the Base16Decoder class.</summary>
		public Base16Decoder() : this(Base16Encoder.StandardAlphabet) { }

		/// <summary>Initializes a new instance of the Base16Decoder class.</summary>
		/// <param name="alphabet">The alphabet to use during encoding.</param>
		public Base16Decoder(char[] alphabet) {
			Alphabet = alphabet;
		}


		/// <summary>Gets a value indicating whether the decoding is case sensitive.</summary>
		override public bool IsCaseSensitive {
			get { return false; }
		}


		/// <summary>Returns the Encoder that corresponds with this Decoder.</summary>
		/// <returns>A new instance of the corresponding Encoder class.</returns>
		override public Encoder GetEncoder() {
			return new Base16Encoder();
		}


		/// <summary>Initializes the Decoder in preparation for a decoding operation.</summary>
		override public void Initialize() {
			resultBuffer = new List<byte>();
			lastNibble = NoNibbleCached;
		}


		/// <summary>Decodes an array of characters.</summary>
		/// <param name="array">The array of characters to decode.</param>
		/// <param name="offset">The index in the array to start decoding from.</param>
		/// <param name="length">The number of characters in the array to decode.</param>
		override public void DecodeCore(char[] array, int offset, int length) {
			for (int i = offset; i < (offset + length); i++) {
				AddNibble(TranslateCharacter(array[i]));
			}
		}


		/// <summary>Completes the decoding operation and returns the result.</summary>
		/// <returns>The result of the decoding operation.</returns>
		/// <exception cref="InvalidOperationException">When there are leftover bits in the buffer (not enough characters were decoded).</exception>
		override public byte[] DecodeFinal() {
			if (lastNibble != NoNibbleCached) {
				throw new InvalidOperationException(Properties.Resources.leftoverDecodedBits);
			}
			return resultBuffer.ToArray();
		}


		/// <summary>Adds a decoded nibble to the buffer.</summary>
		/// <param name="nibble">The nibble to add.</param>
		/// <remarks>Only the low-order 4 bits of the byte are used.</remarks>
		protected void AddNibble(byte nibble) {
			if (lastNibble == NoNibbleCached) {
				lastNibble = nibble & 0x0F;
			} else {
				resultBuffer.Add((byte)((lastNibble << 4) | (nibble & 0x0F)));
				lastNibble = NoNibbleCached;
			}
		}


		/// <summary>Determines the byte value for the specified character.</summary>
		/// <param name="input">The character to translate.</param>
		/// <returns>The byte value for the character in the defined Alphabet.</returns>
		/// <exception cref="ArgumentException">When the character is note defined in the current Alphabet.</exception>
		virtual protected byte TranslateCharacter(char input) {
			int idx = Array.IndexOf(alphabet, char.ToUpper(input, System.Globalization.CultureInfo.InvariantCulture));
			if (idx < 0) {
				throw new ArgumentException(Properties.Resources.illegalCharacter, "input");
			}
			return (byte)idx;
		}
	}
}
