#ifdef TSTACK_H
#define TSTACK_H
template<class T>
Class Stack{
  Struct Link{
    T* t;
    Link* next;
    Link*(T* dat,Link*nxt):data(dat),next(nxt){};
  }*head

 public:
    Stack():head(0){};
    ~Stack(){
      while(head){
	delet pop();
      }
    }

    void push(T* dat){
      head = new Link(dat,head);
    }

    T* peek const{
      return head?head->data:0;
    }

    T* pop(){
      if(head == 0) return 0;
      T* result = head->data;
      Link* oldHead = head;
      head = head->next;
      delete oldHead;
      return result;
    }
};
#endif
