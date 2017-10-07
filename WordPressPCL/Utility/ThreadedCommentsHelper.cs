using System;
using System.Collections.Generic;
using System.Text;
using WordPressPCL.Models;
using System.Linq;
using Newtonsoft.Json;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class to creat threaded comment views
    /// </summary>
    public static class ThreadedCommentsHelper
    {
        /// <summary>
        /// This method returns the comments sorted for a threaded view (oldest first)
        /// inlcuding the depth of a comment
        /// </summary>
        public static List<CommentThreaded> GetThreadedComments(IEnumerable<Comment> comments)
        {
            if (comments == null)
                return null;

            var threadedCommentsFinal = new List<CommentThreaded>();
            var dateSortedThreadedComments = DateSortedWithDepth(comments);

            int lastrun = int.MaxValue;
            while (dateSortedThreadedComments.Count > 0)
            {
                var thisrun = dateSortedThreadedComments.Count;
                if (thisrun == lastrun)
                {
                    // no comments could be moved, abort
                    break;
                }
                lastrun = thisrun;
                foreach (var comment in dateSortedThreadedComments)
                {
                    if (comment.ParentId == 0)
                    {
                        threadedCommentsFinal.Add(comment);
                    }
                    else
                    {
                        // is parent already in threadedComments?
                        var parentComment = threadedCommentsFinal.Find(x => x.Id == comment.ParentId);
                        if (parentComment != null)
                        {
                            var index = threadedCommentsFinal.IndexOf(parentComment);
                            threadedCommentsFinal.Insert(index + 1, comment);
                        }
                    }

                }

                // remove all comments that have been moved to the new sorted list
                foreach (var comment in threadedCommentsFinal)
                {
                    var c = dateSortedThreadedComments.Find(x => x.Id == comment.Id);
                    if (c != null)
                    {
                        dateSortedThreadedComments.Remove(c);
                    }
                    if (dateSortedThreadedComments.Count == 0)
                    {
                        break;
                    }
                }
            }
            return threadedCommentsFinal;
        }

        private static List<CommentThreaded> DateSortedWithDepth(IEnumerable<Comment> comments)
        {
            var dateSortedComments = comments.OrderBy(x => x.Date).ToList();
            var dateSortedthreadedComments = new List<CommentThreaded>();
            foreach (var c in dateSortedComments)
            {
                var serialized = JsonConvert.SerializeObject(c);
                CommentThreaded commentThreaded = JsonConvert.DeserializeObject<CommentThreaded>(serialized);
                commentThreaded.Depth = GetCommentThreadedDepth(c, comments.ToList());
                dateSortedthreadedComments.Add(commentThreaded);
            }
            return dateSortedthreadedComments;
        }

        private static int GetCommentThreadedDepth(Comment comment, List<Comment> list)
        {
            return GetCommentThreadedDepthRecursive(comment, list, 0);
        }

        private static int GetCommentThreadedDepthRecursive(Comment comment, List<Comment> list, int depth)
        {
            if (comment.ParentId == 0)
            {
                return depth;
            }
            else
            {
                var parentComment = list.Find(x => x.Id == comment.ParentId);
                if (parentComment == null)
                {
                    return depth;
                }
                else
                {
                    return GetCommentThreadedDepthRecursive(parentComment, list, depth + 1);
                }

            }
        }
    }
}
