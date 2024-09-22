using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class to create threaded comment views
    /// </summary>
    public static class ThreadedCommentsHelper
    {
        /// <summary>
        /// This method returns the comments sorted for a threaded view (oldest first)
        /// including the depth of a comment
        /// </summary>
        /// <param name="comments">list of comments which will be ordered</param>
        /// <param name="maxDepth">max hierarchy depth</param>
        /// <param name="isDescending">order by descending</param>
        public static List<CommentThreaded> GetThreadedComments(List<Comment> comments, int maxDepth = int.MaxValue, bool isDescending = false)
        {
            if (comments == null)
            {
                return null;
            }

            List<CommentThreaded> threadedCommentsFinal = new();
            List<CommentThreaded> dateSortedThreadedComments = DateSortedWithDepth(comments, maxDepth, isDescending);

            int lastrun = int.MaxValue;
            while (dateSortedThreadedComments.Count > 0)
            {
                int thisrun = dateSortedThreadedComments.Count;
                if (thisrun == lastrun)
                {
                    // no comments could be moved, abort
                    break;
                }
                lastrun = thisrun;
                foreach (CommentThreaded comment in dateSortedThreadedComments)
                {
                    if (comment.ParentId == 0)
                    {
                        threadedCommentsFinal.Add(comment);
                    }
                    else
                    {
                        // is parent already in threadedComments?
                        CommentThreaded parentComment = threadedCommentsFinal.Find(x => x.Id == comment.ParentId);
                        if (parentComment != null)
                        {
                            int index = threadedCommentsFinal.IndexOf(parentComment);
                            threadedCommentsFinal.Insert(index + 1, comment);
                        }
                    }
                }

                // remove all comments that have been moved to the new sorted list
                foreach (CommentThreaded comment in threadedCommentsFinal)
                {
                    CommentThreaded c = dateSortedThreadedComments.Find(x => x.Id == comment.Id);
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

        private static List<CommentThreaded> DateSortedWithDepth(List<Comment> comments, int maxDepth, bool isDescending = false)
        {
            List<Comment> dateSortedComments = isDescending ? comments.OrderByDescending(x => x.Date).ToList()
                                                    : comments.OrderBy(x => x.Date).ToList();
            List<CommentThreaded> dateSortedthreadedComments = new();
            foreach (Comment c in dateSortedComments)
            {
                string serialized = JsonConvert.SerializeObject(c);
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
                Comment parentComment = list.Find(x => x.Id == comment.ParentId);
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
        public static List<CommentThreaded> ToThreaded(this List<Comment> comments, bool isDescending = false)
        {
            return GetThreadedComments(comments, int.MaxValue, isDescending);
        }
    }
}
