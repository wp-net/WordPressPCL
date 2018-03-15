using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WordPressPCL.Models;

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
        /// <param name="comments">list of comments which will be ordered</param>
        /// <param name="maxDepth">max hierachy depth</param>
        /// <param name="isDescending">order by descending</param>
        public static List<CommentThreaded> GetThreadedComments(IEnumerable<Comment> comments, int maxDepth = int.MaxValue, bool isDescending = false)
        {
            if (comments == null)
                return null;

            var threadedCommentsFinal = new List<CommentThreaded>();
            var dateSortedThreadedComments = DateSortedWithDepth(comments, maxDepth, isDescending);

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

        private static List<CommentThreaded> DateSortedWithDepth(IEnumerable<Comment> comments, int maxDepth, bool isDescending = false)
        {
            var dateSortedComments = isDescending ? comments.OrderByDescending(x => x.Date).ToList()
                                                    : comments.OrderBy(x => x.Date).ToList();
            var dateSortedthreadedComments = new List<CommentThreaded>();
            foreach (var c in dateSortedComments)
            {
                var serialized = JsonConvert.SerializeObject(c);
                CommentThreaded commentThreaded = JsonConvert.DeserializeObject<CommentThreaded>(serialized);
                commentThreaded.Depth = GetCommentThreadedDepth(c, comments.ToList(), maxDepth);
                dateSortedthreadedComments.Add(commentThreaded);
            }
            return dateSortedthreadedComments;
        }

        private static int GetCommentThreadedDepth(Comment comment, List<Comment> list, int maxDepth)
        {
            return GetCommentThreadedDepthRecursive(comment, list, 0, maxDepth);
        }

        private static int GetCommentThreadedDepthRecursive(Comment comment, List<Comment> list, int depth, int maxDepth)
        {
            if (comment.ParentId == 0)
            {
                return Math.Min(depth, maxDepth);
            }
            else
            {
                var parentComment = list.Find(x => x.Id == comment.ParentId);
                if (parentComment == null)
                {
                    return Math.Min(depth, maxDepth);
                }
                else
                {
                    return GetCommentThreadedDepthRecursive(parentComment, list, depth + 1, maxDepth);
                }
            }
        }

        /// <summary>
        /// Extension method: Get Threaded comments from ordinary comments
        /// </summary>
        /// <param name="comments">Comments which will be threaded</param>
        /// <param name="isDescending">Newest comments should be shown first</param>
        /// <returns>List of threaded comments</returns>
        public static List<CommentThreaded> ToThreaded(this IEnumerable<Comment> comments, bool isDescending = false)
        {
            return GetThreadedComments(comments, int.MaxValue, isDescending);
        }
    }
}