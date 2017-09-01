Exercise 3 for system integration intro: 
====================================
###### Kristian
```
call Customer order
call SequenceTagger order orderChannel "/Order/@OrderID" 
call Enricher orderChannel brevChannel
call Splitter brevChannel brevReady "/Order/Item"
call  Router brevReady coldChannel "Item = 'FRAPPUCINO'" hotChannel
start bin\FormRunners Delay "HotBevBarista" Barista hotChannel orderCompletedChannelTotal 400
start bin\FormRunners Delay "ColdBevBarista " Barista coldChannel orderCompletedChannelTotal 800
call Tee orderCompletedChannelTotal orderCompletedChannelTotal2 orderCompletedChannelTotal3
call Logger orderCompletedChannelTotal3
call Aggregator orderCompletedChannelTotal2 orderCompletedChannel "/Item/@OrderID" 
call Logger orderCompletedChannel(edited)
```
###### Hauge
```
call customer orderChannel 
call Logger CompleteChannel 
call SequenceTagger orderChannel orderReadyChannel "/Order/@OrderID" 
call Enricher orderReadyChannel orderEnriched 
call Tee orderEnriched Splitchannel CompleteChannel 
call Splitter Splitchannel ItemChannel "/Order/Item" 
call Router ItemChannel coldChannel "Item = 'FRAPPUCINO'" hotChannel 
call HotBevBarista hotChannel CompleteItemChannel 
call ColdBevBarista coldChannel CompleteItemChannel 
call Aggregator CompleteItemChannel CompleteChannel "/Item/@OrderID"
```
