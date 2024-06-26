﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<Comment> GetListWithAppUserByDestination(int id)
        {
            return _commentDal.GetListWithAppUserByDestination(id);
        }

        public List<Comment> GetListWithDestination()
        {
            return _commentDal.GetListWithDestination();
        }

        public void TAdd(Comment t)
        {
            _commentDal.Insert(t);
        }

        public void TDelete(Comment t)
        {
            _commentDal.Delete(t);
        }

        public Comment TGetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> TGetList()
        {
            return _commentDal.GetList();
        }

        public List<Comment> TGetListByFilter(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.GetListByFilter(filter);
        }

        public List<Comment> TGetListByFilterWithDestination(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.TGetListByFilterWithDestination(filter);
        }

        public void TUpdate(Comment t)
        {
            _commentDal.Update(t);
        }
    }
}
