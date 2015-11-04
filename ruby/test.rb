#!/usr/bin/env ruby


for i in  1..1000
       j =  i*i
       printf("%4d^2 = %07d/",i,j)

       if(i %4 ==0) then
            printf("\n")
       end
end
