using System.Runtime.Serialization;
namespace CourseOrderWcf
{
  [DataContract]
  public class Course : BindableBase
  {
    private string title;

    [DataMember]
    public string Title
    {
      get { return title; }
      set
      {
        SetProperty(ref title, value);
      }
    }
  }

}
