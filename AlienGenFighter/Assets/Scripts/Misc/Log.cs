#define TRACE_ENTITY
//#define 

using System.Diagnostics;

namespace Assets.Scripts.Misc {
    /// <summary>
    /// Provides a set of methods and properties that help you trace the execution of your code. This class cannot be inherited.
    /// </summary>
    public static class Log {
        #region info
        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// <para>Conditional compilation : ALL, INFO</para>
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
        [Conditional("ALL"), Conditional("INFO")]
        public static void info(string format, params object[] arg) {
            UnityEngine.Debug.LogFormat("[INFO]\n{0}", string.Format(format, arg));
        }
        #endregion info

        #region debug
        /// <summary>
        /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
        /// <para>Conditional compilation : ALL, DEBUG</para>
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
        [Conditional("ALL"), Conditional("DEBUG")]
        public static void debug(string format, params object[] arg) {
            UnityEngine.Debug.LogFormat("[DEBUG]\n{0}", string.Format(format, arg));
        }

        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// </summary>
        public static class Debug {
            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, DEBUG_MAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("DEBUG"), Conditional("DEBUG_MAP")]
            public static void Map(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[DEBUG.MAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, DEBUG_SQUAREMAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("DEBUG"), Conditional("DEBUG_SQUAREMAP")]
            public static void SquareMap(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[DEBUG.SQUAREMAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, DEBUG_ENTITY</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("DEBUG"), Conditional("DEBUG_ENTITY")]
            public static void Entity(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[DEBUG.ENTITY]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, DEBUG_UI</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("DEBUG"), Conditional("DEBUG_UI")]
            public static void Ui(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[DEBUG.UI]\n{0}", string.Format(format, arg));
            }
        }
        #endregion debug

        #region trace
        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// <para>Conditional compilation : ALL, TRACE, DEBUG</para>
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
        [Conditional("ALL"), Conditional("TRACE"), Conditional("DEBUG")]
        public static void trace(string format, params object[] arg) {
            UnityEngine.Debug.LogFormat("[TRACE]\n{0}", string.Format(format, arg));
        }

        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// </summary>
        public static class Trace {
            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, TRACE, TRACE_MAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("TRACE_MAP")]
            public static void Map(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[TRACE.MAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, TRACE, TRACE_SQUAREMAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("TRACE_SQUAREMAP")]
            public static void SquareMap(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[TRACE.SQUAREMAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, TRACE, TRACE_MAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("TRACE_ENTITY")]
            public static void Entity(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[TRACE.ENTITY]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, DEBUG, TRACE, TRACE_Ui</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("TRACE_UI")]
            public static void Ui(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[TRACE.UI]\n{0}", string.Format(format, arg));
            }
        }

        #endregion trace

        #region warning
        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN</para>
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
        [Conditional("ALL"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG")]
        public static void warning(string format, params object[] arg) {
            UnityEngine.Debug.LogWarningFormat("[WARN]\n{0}", string.Format(format, arg));
        }

        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// </summary>
        public static class Warning {
            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, WARN_MAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("WARN_MAP")]
            public static void Map(string format, params object[] arg) {
                UnityEngine.Debug.LogWarningFormat("[WARN.MAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, WARN_SQUAREMAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("WARN_SQUAREMAP")]
            public static void SquareMap(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[WARN.SQUAREMAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, WARN_ENTITY</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("WARN_ENTITY")]
            public static void Entity(string format, params object[] arg) {
                UnityEngine.Debug.LogWarningFormat("[WARN.ENTITY]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, WARN_UI</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("WARN_UI")]
            public static void Ui(string format, params object[] arg) {
                UnityEngine.Debug.LogWarningFormat("[WARN.UI]\n{0}", string.Format(format, arg));
            }
        }
        #endregion warning

        #region error
        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, ERROR</para>
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
        [Conditional("ALL"), Conditional("ERROR"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG")]
        public static void error(string format, params object[] arg) {
            UnityEngine.Debug.LogErrorFormat("[ERROR]\n{0}", string.Format(format, arg));
        }

        /// <summary>
        /// Provides a set of methods and properties that help debug your code. This class cannot be inherited.
        /// </summary>
        public static class Error {
            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, ERROR, ERROR_MAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("ERROR"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("ERROR_MAP")]
            public static void Map(string format, params object[] arg) {
                UnityEngine.Debug.LogErrorFormat("[ERROR.MAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, ERROR, ERROR_SQUAREMAP</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("ERROR"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("ERROR_SQUAREMAP")]
            public static void SquareMap(string format, params object[] arg) {
                UnityEngine.Debug.LogFormat("[ERROR.SQUAREMAP]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, ERROR, ERROR_ENTITY</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("ERROR"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("ERROR_ENTITY")]
            public static void Entity(string format, params object[] arg) {
                UnityEngine.Debug.LogErrorFormat("[ERROR.ENTITY]\n{0}", string.Format(format, arg));
            }

            /// <summary>
            /// Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream.
            /// <para>Conditional compilation : ALL, TRACE, DEBUG, WARN, ERROR, ERROR_UI</para>
            /// </summary>
            /// <param name="format">A composite format string</param>
            /// <param name="arg">An object array that contains zero or more objects using <c>format</c>.</param>
            [Conditional("ALL"), Conditional("ERROR"), Conditional("WARN"), Conditional("TRACE"), Conditional("DEBUG"), Conditional("ERROR_UI")]
            public static void Ui(string format, params object[] arg) {
                UnityEngine.Debug.LogErrorFormat("[ERROR.UI]\n{0}", string.Format(format, arg));
            }
        }
        #endregion error
    }
}