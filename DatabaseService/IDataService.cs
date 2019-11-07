using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public interface IDataService
  {
    List<Question> GetQuestions(PagingAttributes pagingAttributes);
    Question GetQuestion(int id);
    int NumberOfQuestions();
    List<Post> GetPosts();
    List<AnswerDto> GetAnswers();
    object GetQuestionWithAnswers(int questionId);
    // List<Answer> GetAnswersForQuestion(int questionId);
    // List<AnswerDto> GetAnswersForQuestion(int questionId);



    // Category CreateCategory(string name, string description);
    // // Category CreateCategory(Category category);
    // bool DeleteCategory(int id);
    // List<Category> GetCategories();
    // Order GetOrder(int id);
    // List<OrderDetails> GetOrderDetailsByOrderId(int id);
    // List<OrderDetails> GetOrderDetailsByProductId(int id);
    // List<Order> GetOrders();
    // Product GetProduct(int id);
    // List<Product> GetProductByCategory(int id);
    // List<Product> GetProductByName(string name);
    // bool UpdateCategory(int id, string name, string description);
    // bool PutCategory(int id, string name, string description);
  }
}