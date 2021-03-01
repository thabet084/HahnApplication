import boto3

client = boto3.client('autoscaling',region_name='ohio')
try:
    response = client.start_instance_refresh(
        AutoScalingGroupName='pizza-asg',
        Strategy='Rolling',
        Preferences={
            'MinHealthyPercentage': 90,
            'InstanceWarmup': 0
        }
     )
    print(response)
except:
  print("Something else went wrong")
