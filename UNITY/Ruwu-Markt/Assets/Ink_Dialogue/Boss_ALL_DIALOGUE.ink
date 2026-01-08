EXTERNAL AdvanceQuest(endingType)

VAR FinalQuestState = "NOT RUNNING"

===BOSS===
{ FinalQuestState :
    - "NOT RUNNING": -> Not_Running
    - "GOOD ENDING": -> Good_Ending
    - "BAD ENDING": -> Bad_Ending
}


=Not_Running
What do you want?

I don't have time right now.

Get back to work!
*[Yes Chef :(]
    Good slave.
*[Fuk U!!!]
    Owie my feelings!
*[*Spit in his face*]
    AAARRRGHHHH!!!!!
- -> END



=Good_Ending
Yes?

*[I need to speak with you]
    Really?
    -> Good_Stitch_One
    
    = Good_Stitch_One
        *[It's about work.]
            Why bother me with it?
            
            I'm sure it's not that important.

            -> Good_Stitch_Two
    
    =Good_Stitch_Two
        *[I want to quit]
            WHAT?!
            
            YOU CAN'T QUIT!
            
            YOU'RE MY BEST WORKER!
            
            PLEASE STAY!
            
            I CAN'T RUN THE STORE WITHOUT YOU!
            
            I BEG YOU!
            
            PLEASE!
            -> Good_Stitch_Three
    
    =Good_Stitch_Three
        *[No, look for someone else to exploit]
            YOU WILL RUIN ME!
            
            YOU'RE GOING TO REGRET THIS!
            
            EVENTUALLY YOU WILL COME CRAWLING BACK!
            
            AND WHEN YOU DO, I WILL LEAVE YOU TO ROT, JUST LIKE YOU LEFT ME!
            
            YOU NEED ME!
            
            YOU'RE NOTHING WITHOUT ME!
            
            HAHAHAHAHAHAHAHAHAHA!
            
            ~AdvanceQuest(1)
            ->END
        
        *[Fine, I will stay]
            Of course, you were just joking.
            
            I knew you didn't have it in you.
            
            You need me and this job.
            
            See you next week!
            
            ~AdvanceQuest(3)
            ->END
            
            



=Bad_Ending
Yes?

*[You wanted to speak with me?]
    Ah yes, of course.
    ->Bad_Stitch_One
    
    =Bad_Stitch_One
        It's about your job here.
        
        You see, I feel like you have been slacking off in recent times.
        
        And I feel like you're not worth the money.
        
        So I decided that you're fired.
        
        Immediately.
        
        Good bye!
        
        ~AdvanceQuest(2)
        -> END