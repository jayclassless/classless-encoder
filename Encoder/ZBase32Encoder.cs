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
	/// <summary>An implementation of the ZBase32 encoding algorithm.</summary>
	public class ZBase32Encoder : Base32Encoder {
		/// <summary>The standard alphabet defined for ZBase32.</summary>
		new static public readonly char[] StandardAlphabet = new char[32] {
			'y', 'b', 'n', 'd', 'r', 'f', 'g', '8',
			'e', 'j', 'k', 'm', 'c', 'p', 'q', 'x',
			'o', 't', '1', 'u', 'w', 'i', 's', 'z',
			'a', '3', '4', '5', 'h', '7', '6', '9',
		};


		/// <summary>Initializes a new instance of the ZBase32HexEncoder class.</summary>
		public ZBase32Encoder() : base(StandardAlphabet, Base32HexEncoder.StandardPadding) { }


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			return new ZBase32Decoder();
		}


		/// <summary>Completes the encoding operation and returns the result.</summary>
		/// <returns>The result of the encoding operation.</returns>
		override public string EncodeFinal() {
			return base.EncodeFinal().TrimEnd(StandardPadding);
		}
	}
}
