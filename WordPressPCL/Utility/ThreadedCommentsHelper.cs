using System;
using System.Collections.Generic;
using System.Text;
using WordPressPCL.Models;
using System.Linq;
using Newtonsoft.Json;

namespace WordPressPCL.Utility
{
    public static class ThreadedCommentsHelper
    {
        public static List<CommentThreaded> GetThreadedComments(IEnumerable<Comment> comments)
        {
            var threadedComments = new List<CommentThreaded>();

            var sortedComments = comments.OrderBy(x => x.Date).ToList();

            int depth = 0;
            while(sortedComments.Count > 0)
            {

                foreach (var comment in sortedComments)
                {
                    var serialized = JsonConvert.SerializeObject(comment);
                    CommentThreaded commentThreaded = JsonConvert.DeserializeObject<CommentThreaded>(serialized);
                    commentThreaded.Depth = GetCommentThreadedDepth(comment, comments.ToList());

                    if (comment.ParentId == 0)
                    {
                        threadedComments.Add(commentThreaded);
                    }
                    else
                    {
                        // is parent already in threadedComments?
                        var parentComment = threadedComments.Find(x => x.Id == comment.ParentId);
                        if(parentComment != null)
                        {
                            var index = threadedComments.IndexOf(parentComment);
                            threadedComments.Insert(index + 1, commentThreaded);
                        }
                    }

                }

                // remove all comments that have been moved to the new sorted list
                foreach (var comment in threadedComments)
                {
                    var c = sortedComments.Find(x => x.Id == comment.Id);
                    if (c != null)
                    {
                        sortedComments.Remove(c);
                    }
                    if(sortedComments.Count == 0)
                    {
                        break;
                    }
                }
            }


            return threadedComments;
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
                return GetCommentThreadedDepthRecursive(parentComment, list, depth + 1);
            }
        }
    }
}
