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
	/// <summary>An implementation of the Base64 encoding algorithm using the URL and Filename Safe alphabet.</summary>
	public class Base64UrlEncoder : Base64Encoder {
		/// <summary>The URL and Filename Safe alphabet defined by RFC4648.</summary>
		new static public readonly char[] StandardAlphabet = new char[64] {
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
			'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
			'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
			'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
			'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
			'w', 'x', 'y', 'z', '0', '1', '2', '3',
			'4', '5', '6', '7', '8', '9', '-', '_',
		};


		/// <summary>Initializes a new instance of the Base64UrlEncoder class.</summary>
		public Base64UrlEncoder() : base(StandardAlphabet, StandardPadding) { }


		/// <summary>Returns the Decoder that corresponds with this Encoder.</summary>
		/// <returns>A new instance of the corresponding Decoder class.</returns>
		override public Decoder GetDecoder() {
			return new Base64UrlDecoder();
		}


		/// <summary>Completes the encoding operation and returns the result.</summary>
		/// <returns>The result of the encoding operation.</returns>
		override public string EncodeFinal() {
			return base.EncodeFinal().TrimEnd(StandardPadding);
		}
	}
}
