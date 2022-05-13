using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResearchGate.Models;

namespace ResearchGate.datafunctionfolder
{
    public class datafunction
    {
        public string Like(int id, bool status)
        {
            using (var db = new dbreserchgateEntities())
            {
                var Research = db.tbl_paper.FirstOrDefault(x => x.p_id == id);
                var toggle = false;
                tbl_likes like = db.tbl_likes.FirstOrDefault(x => x.PaperId == id && x.UserId == x.UserId);

                //checking which button was pressed
                if (like == null)
                {

                    like = new Models.tbl_likes();
                    like.UserId = like.UserId;
                    like.l_islike = status;
                    like.PaperId = id;
                    if (status)
                    {
                        if (Research.p_LikeCount == null) //if no previous likes or dislikes
                        {
                            Research.p_LikeCount = Research.p_LikeCount ?? 0 + 1;
                            Research.p_DislikeCount = Research.p_DislikeCount ?? 0;



                        }
                        else
                        {
                            Research.p_LikeCount = Research.p_LikeCount + 1;
                        }

                    }
                    else
                    {
                        if (Research.p_DislikeCount == null)
                        {
                            Research.p_DislikeCount = Research.p_DislikeCount ?? 0 + 1;
                            Research.p_LikeCount = Research.p_LikeCount ?? 0;
                        }
                        else
                        {
                            Research.p_DislikeCount = Research.p_DislikeCount + 1;

                        }
                    }
                    db.tbl_likes.Add(like);

                }
                else
                {
                    toggle = true;
                }
                if (toggle)
                {
                    like.UserId = like.UserId;
                    like.l_islike = status;
                    like.PaperId = id;
                    if (status)
                    {
                        //if user wants to change his dislike to a like ( need to increase likes by 1 and decrease dislikes by 1 )
                        Research.p_LikeCount = Research.p_LikeCount + 1;
                        if (Research.p_DislikeCount == 0 || Research.p_DislikeCount < 0)
                        {
                            Research.p_DislikeCount = 0;
                        }
                        else
                        {
                            Research.p_DislikeCount = Research.p_DislikeCount - 1;
                        }
                    }
                    else
                    {
                        // if user wants to change his like to dislike ( need to increase dislikes by 1 and decrease likes by 1)
                        Research.p_DislikeCount = Research.p_DislikeCount + 1;
                        if (Research.p_LikeCount == 0 || Research.p_LikeCount < 0)
                        {
                            Research.p_LikeCount = 0;
                        }
                        else
                        {
                            Research.p_LikeCount = Research.p_LikeCount - 1;
                        }
                    }
                }
                db.SaveChanges();
                return Research.p_LikeCount + "/" + Research.p_DislikeCount;

            }

        }



        public int? GetLikesCount(int? id) //to count likes
        {
            using (var db = new dbreserchgateEntities())
            {
                var count = (from x in db.tbl_paper where (x.p_id == id && x.p_LikeCount != null) select x.p_LikeCount).FirstOrDefault();
                return count;

            }

        }



        public int? GetDislikesCount(int? id) // to count dislikes
        {
            using (var db = new dbreserchgateEntities())
            {
                var count = (from x in db.tbl_paper where (x.p_id == id && x.p_DislikeCount != null) select x.p_DislikeCount).FirstOrDefault();
                return count;
            }

        }



        public List<tbl_likes> GetAllUsers(int? id) //get all users who interacted with research
        {
            using (var db = new dbreserchgateEntities())
            {
                var count = (from x in db.tbl_likes where x.PaperId == id select x).ToList();
                return count;
            }
        }
    }


}