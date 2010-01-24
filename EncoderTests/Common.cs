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
using System.Collections;
using System.Reflection;
using NUnit.Framework;

namespace Classless.Encoder.Tests {
	sealed class Common {
		static public IEnumerable EncoderNames {
			get {
				Assembly classless = Assembly.GetAssembly(typeof(Encoder));
				foreach (Type type in classless.GetTypes()) {
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(Encoder)))) {
						yield return new TestCaseData(type.FullName, type);
						yield return new TestCaseData(type.Name, type);
					}
				}
			}
		}


		static public IEnumerable DecoderNames {
			get {
				Assembly classless = Assembly.GetAssembly(typeof(Decoder));
				foreach (Type type in classless.GetTypes()) {
					if ((!type.IsAbstract) && (IsDescendant(type, typeof(Decoder)))) {
						yield return new TestCaseData(type.FullName, type);
						yield return new TestCaseData(type.Name, type);
					}
				}
			}
		}


		static public bool IsDescendant(Type type, Type ancestor) {
			if (type.BaseType == null) {
				return false;
			} else if (type.BaseType == ancestor) {
				return true;
			} else {
				return IsDescendant(type.BaseType, ancestor);
			}
		}


		static public void AreEqual(Array expected, Array actual) {
			Assert.AreEqual(expected.Length, actual.Length, "Expected array length {0} but was {1}.", expected.Length, actual.Length);

			for (int i = 0; i < expected.Length; i++) {
				Assert.AreEqual(
					expected.GetValue(i),
					actual.GetValue(i),
					string.Format("Arrays differ at index {0}.", i)
				);
			}
		}
	}
}
