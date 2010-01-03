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

namespace Classless.Encoder {
	/// <summary>An implementation of the ZBase32 decoding algorithm.</summary>
	public class ZBase32Decoder : Base32Decoder {
		/// <summary>Initializes a new instance of the ZBase32HexDecoder class.</summary>
		public ZBase32Decoder() : base(ZBase32Encoder.StandardAlphabet, Base32Encoder.StandardPadding) { }


		/// <summary>Gets a value indicating whether the decoding is case sensitive.</summary>
		override public bool IsCaseSensitive {
			get { return true; }
		}


		/// <summary>Returns the Encoder that corresponds with this Decoder.</summary>
		/// <returns>A new instance of the corresponding Encoder class.</returns>
		override public Encoder GetEncoder() {
			return new ZBase32Encoder();
		}


		/// <summary>Determines the byte value for the specified character.</summary>
		/// <param name="input">The character to translate.</param>
		/// <returns>The byte value for the character in the defined Alphabet.</returns>
		/// <exception cref="ArgumentException">When the character is note defined in the current Alphabet.</exception>
		override protected byte TranslateCharacter(char input) {
			int idx = Array.IndexOf(Alphabet, input);
			if (idx < 0) {
				throw new ArgumentException(Properties.Resources.illegalCharacter, "input");
			}
			return (byte)idx;
		}
	}
}
