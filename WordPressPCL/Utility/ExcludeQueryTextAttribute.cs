using System;

namespace WordPressPCL.Utility {
    /// <summary>
    /// Attribute to exclude query text in querybuilder
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcludeQueryTextAttribute : Attribute {

        /// <summary>
        /// Value which determines if query text is excluded
        /// </summary>
        public string ExclusionValue { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exclusionValue">value which determines if query text is excluded</param>
        public ExcludeQueryTextAttribute(string exclusionValue) {
            ExclusionValue = exclusionValue;
        }
    }
}