﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsaneIO.Insane.Extensions;
using InsaneIO.Insane.Cryptography;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.Tests
{
    [RequiresPreviewFeatures]
    [TestClass]
    public class RsaExtensionsUnitTests
    {
        public const string Data = "HelloWorld!!!";
        public const uint KeySize = 4096u;
        public const string PublicKeyBer = "MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEA8ujlgTI2uLY/tb52ZWXRjy3ExaGRO4vci/ZPQmTR6tx1bIMLgR9X66zl0FxrXzeoUxxla3VevNNZ0lQX92lNI5J+eEfxBgRbVQCxqoJwcMD/PClKL8P/VmBKIhL+EKTh652Cs3YcoBhMys1oQC5qISp7e8YLhRQZA0DpeFT+l/ToOUUGdo5eDttiQWAr+x4aZTolSLrmc4818P2Z5B3pv2eL9B36+liO5xt7tX10kCZBiL1NIGlLTIrvR4fB81tCeQTXnUKn38oN9BlKjXyTPxqlOe/NbeUK5fBBFpZYFJl33Gknx6YRtNOiFyNQoL0GfVpyoLHtSNFC/GY+SiJprOTugrxIg4E0bilLZHE3JRMoUnFb1ZUgpbRmp54GXy2OePU3z+1lhN/aeCepCapwc1rGqbq3zjgsKEs+rhE87ZTZKibSvKUZfDqYClHbFJnYTsNdCsmTqc6wEmJra0hCQgSVKXdJqogqlM1VfbbLsR6TqaK6c/8upv/WFGU7jH0Lp9q9UCwdLvHZ9YoI6VR9SiX1t1PYP62YX+qp1T0TfSZdqw6mzefBgQ8SjFl3qWekN/OO3+VcDGUHk82w6xmJb4GQmvSZuDM120COhVMmSumMWl1o1U7/IXe5y8GvaWkQVm2eveSkK9n5ZWO1AEiwBMqYEI4qTxhH714mB3L5viUCAwEAAQ==";
        public const string PrivateKeyBer = "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";
        public const string PublicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAs9tvf8KaGRfhsAugF6n0\r\nhjR8aH5f08idS6o/9L/FBIHe2acEyBZPy/i7d7Z4YWx/dSW7fKtiahOEM/bt0Uif\r\nrdDCEE7+P/SU1p5ORJ03sXrMTkTXBAbeP9w8WMoqajH6z2+3PDuGxysoOVj5ZyO6\r\nSiWekJmkXUX7Y8dnWFjP5Ofe8po0vRmj6Uv3YAvHaOw6M+cDQ+lxUApZuYAe1eRQ\r\n96BURMZqIytpDSouYqBlPKGrYsXtV0+vAE80I/SKNK5HtRiv0nmosHp5rEMlgo04\r\njUcDLXcZy42Mvx7kRLeBbZUNcSvnJnBkohKPlQ5TCBWyEDxIAu3kpCFuMwBlZBDA\r\nDVLykiB2C0PoEbvP5X+gteHH3NjhY6m9U4nJOy/PVbpL8t25QHVPRgLVV3JCEtI5\r\n8wLiUKJ9J3l4HOmDT1zcnRxvqaxN0YvxyfwrZ6GwiQUQWQGI3o4Y+LkMGBxykE1x\r\nUzOmeiTYoTE0VblvJGbWvFenloFEGdJHmEjFo03YDp5/8F16sj5k9Z0OKMyHPj3K\r\nDoKuSAgRiZ6ZDsDFBin+ac6V0WNRhlz56mMn/RSU7vQcWTobZYiJW8QccGKxBVdc\r\nuNhlwMROQxH5ksnl4fNDxNCST40nxXQ7auLyl57BNqtRq+fnLpMgswo4JfqUySnq\r\nTHxWG2FuouuZKFM3dZwEUmUCAwEAAQ==\r\n-----END PUBLIC KEY-----";
        public const string PrivateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nMIIJQQIBADANBgkqhkiG9w0BAQEFAASCCSswggknAgEAAoICAQCz229/wpoZF+Gw\r\nC6AXqfSGNHxofl/TyJ1Lqj/0v8UEgd7ZpwTIFk/L+Lt3tnhhbH91Jbt8q2JqE4Qz\r\n9u3RSJ+t0MIQTv4/9JTWnk5EnTexesxORNcEBt4/3DxYyipqMfrPb7c8O4bHKyg5\r\nWPlnI7pKJZ6QmaRdRftjx2dYWM/k597ymjS9GaPpS/dgC8do7Doz5wND6XFQClm5\r\ngB7V5FD3oFRExmojK2kNKi5ioGU8oatixe1XT68ATzQj9Io0rke1GK/Seaiwenms\r\nQyWCjTiNRwMtdxnLjYy/HuREt4FtlQ1xK+cmcGSiEo+VDlMIFbIQPEgC7eSkIW4z\r\nAGVkEMANUvKSIHYLQ+gRu8/lf6C14cfc2OFjqb1Tick7L89Vukvy3blAdU9GAtVX\r\nckIS0jnzAuJQon0neXgc6YNPXNydHG+prE3Ri/HJ/CtnobCJBRBZAYjejhj4uQwY\r\nHHKQTXFTM6Z6JNihMTRVuW8kZta8V6eWgUQZ0keYSMWjTdgOnn/wXXqyPmT1nQ4o\r\nzIc+PcoOgq5ICBGJnpkOwMUGKf5pzpXRY1GGXPnqYyf9FJTu9BxZOhtliIlbxBxw\r\nYrEFV1y42GXAxE5DEfmSyeXh80PE0JJPjSfFdDtq4vKXnsE2q1Gr5+cukyCzCjgl\r\n+pTJKepMfFYbYW6i65koUzd1nARSZQIDAQABAoICABrbN2kCa/Q3RrH86mjeLe8k\r\nQzdvN2vuVt6Hi4lGYWrs4ZPqhqJCAqRYfdXAX3VcuCOMANT62nUweNsxkg1gJMfV\r\nlkTNJtXx9Y+ej91bBIfx6DP/v4OQavtqLXCsr2ywDd2PtvK9iMCQxy7ZBFTMVvLf\r\nby/0YUC0RHd/vQTKLjDmFrpvIFTkUT9y4ntrBtm5/G7nnes0HoFvKjqy0OfrcdOo\r\nEy0523to7gTTOZ3siXFmSqszFt+kvGL1cLm1uDVpmLeH+ikZlYDUqPp6BE3cPIeM\r\nBQNy910Xw95+BGPmauMsEJfHkHqC6ePxlZMSUn+wjagy+CXH1A5WF5hyBLHOdn7G\r\nojaZCy+ihoh/fp4XojO41ARVKp8DhzF0xqfzKcg6/LpNq3ayqP/WGqCB/W0sbJrN\r\nI679WU22GhL1wCFInJUliK3GAdLVqNXUjJqdgIPbNh7X/h/mnfVOU7E60/gIkEkU\r\nsUhKHIKMwSzqmlJqokA0+kmQ/6JnRG588uGM8uBJXfxvFYzLVJSDR+CvxeT4Xfie\r\nEk71NQLzFgIcBMO1R7FH65Z9yl6X640z0/nGHVrKpTmR8htXJdY0qcazmlDS7qgk\r\nbRDsYYTmgqcGoLqn489GH78QCndH0V185dCa8ZQe+S6Yhy0mq8xDqxlRbO/HIKxS\r\nFjgbCackX6l3n+hbJAh5AoIBAQDkHORfJVBu0d7gMW1VO6ysBZb6PG1gE3p2Y1Ev\r\nFfhRF5/Z13xOLVA4IUpAbXZEXpSCP3wpPW33Y2kUMuSdtkntQWe8VmBLQzB4WIE6\r\nRq3H2f3Uo5jVKvSsbD9NEPOOVwGS5no7W/Ns7pq42UPud1j/1Dl6B7SdRADjsibV\r\nX3A7yDi0VsWB9ebE/TEWdPUKfU/6tCn2Z4M784QkIJFce3IZuZzLnPWF6BABb5oU\r\nXXxGvFEmZF+v22oxgPMYlGn4X7A6hA5PW+GGMFkjeG3PktEQqjxELJCS5QlcmAZY\r\no4CKN78KyLbYKU49TrVwW2ertOkdmL8BFzMTCTW4xXHp9LmjAoIBAQDJ2FCX4kPq\r\n2OfUrwNhsFc+c2WYxdBniFZvN1SijkT1ImWu0PZFlzIJAP9rH0smD3/xAYZwIzwq\r\nc3pWrA3OvpTXp414D3zGyTD/E9m9Zb1sj+0KIUkn9KmUyUuwvjINhYf2Y0N4+phI\r\nVZeJDhSpAy3JtmqZBPPBKBCH8AAce2lhJBOcUotzaXbBI9sZ/JstrUzxsNfQ/3V1\r\nFyYAIaoQIVMRE7Sv9YlnNOC6hzd38lzBXF0Q2IatRpOyu2vHVp5Er2pfsbysckTf\r\nxVA3/Lk8yt9LXTAeZ8meFvza3kTeDCgRKXm3Ptyorpe4rkrojL8yH74FJ4MqJQxi\r\nt6mVcGSdzZRXAoIBAB29JojA9Jt+APB5gSd7gCdtEyHMfvXnlC+bAxqAYr8vtUdR\r\nMIOApNSsgmGj46dGLVZNsIIv4AZAj5JykBt/iGPGAyAqoqMtP0RxRWezjzRe1xjB\r\nu0sPLpbMBfSmIRqNfUcJhSX39uRctw8iRBjytkekA1pFZuaZ1wPYVfe7WYZxs/LO\r\n3TD0PgGwgJXM6aVUcPjeRBo7pIBMXw3WsRy9e0KfUDG7ZMbwWiXVxuCgz3LpWisH\r\nKvJiSJXrmcW6k5Yt42u9i6AM+nnkE7rsGGTXXyotB25b4P1FntLTfwBPUJK0cdOW\r\nF0BXIjwb5sufHwTjsQCKyVv7Ck2RBc/w7cjyfsECggEAbvuc3NaRp5jaaAxPHLC8\r\nV99Vlpn2DGdft0lCJRt6+RqPtH67WupWnbLJvln3lRbir166UABfvau/MdbqxKnv\r\n51/+nmxNnHPVl/uPNt+xNvcwS+ifc/PIJsJD9wutM9gfOS+9UtDfWK6PYtG2NHRG\r\nAepmVHrvmF5yMHybYw3MlJgknEia2ru7i1kuyOwnWwc2DpLBJ+6+48H1qA7aloHs\r\nTbB7ImEsyu29P3LH4hEGRV+G4JNeAfqTjawWCAYtgfC92Z498sfl7PBuOkqgg7e8\r\n7ZBwu9cvediD8chTL5CZm04l2ccbxzgwvmA/WfzByErBzc27BjpWxBTejwe8mzQF\r\ngwKCAQAGnP0YdsHSqEMS5yxXkliGZkQgwUiTDBOwbo6lz4oqUjimYrSU2u6Y+dnt\r\nfMaQpHyFAUETHOVao+AIjHEngOyxLtXYmiYvMf0PNKA3IFhaI4KyKkIaFUF5LSpi\r\nlagb1YERtyY7/eU2Pt9AAjRdPaaVKneiNSVEr+gUkTraLyS3DNhEr2tu8nDCYoC/\r\niV0pcThWhhGaUuSLm3RXQP6P7JXLovEiVuLyeQNA2ZrIPOCJ639Ng9e7X7t6xLDw\r\na7v095Jz3BHWSSimY1PepTqllto+ZZ9RQIhJTIlF0d4vX3Mv/zpbwbCIrxNJQonq\r\n03wz5HlbyAbnbw7IZSBC59GEc3ec\r\n-----END PRIVATE KEY-----";
        public const string PublicKeyPemRsa = "-----BEGIN RSA PUBLIC KEY-----\r\nMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAs9tvf8KaGRfhsAugF6n0\r\nhjR8aH5f08idS6o/9L/FBIHe2acEyBZPy/i7d7Z4YWx/dSW7fKtiahOEM/bt0Uif\r\nrdDCEE7+P/SU1p5ORJ03sXrMTkTXBAbeP9w8WMoqajH6z2+3PDuGxysoOVj5ZyO6\r\nSiWekJmkXUX7Y8dnWFjP5Ofe8po0vRmj6Uv3YAvHaOw6M+cDQ+lxUApZuYAe1eRQ\r\n96BURMZqIytpDSouYqBlPKGrYsXtV0+vAE80I/SKNK5HtRiv0nmosHp5rEMlgo04\r\njUcDLXcZy42Mvx7kRLeBbZUNcSvnJnBkohKPlQ5TCBWyEDxIAu3kpCFuMwBlZBDA\r\nDVLykiB2C0PoEbvP5X+gteHH3NjhY6m9U4nJOy/PVbpL8t25QHVPRgLVV3JCEtI5\r\n8wLiUKJ9J3l4HOmDT1zcnRxvqaxN0YvxyfwrZ6GwiQUQWQGI3o4Y+LkMGBxykE1x\r\nUzOmeiTYoTE0VblvJGbWvFenloFEGdJHmEjFo03YDp5/8F16sj5k9Z0OKMyHPj3K\r\nDoKuSAgRiZ6ZDsDFBin+ac6V0WNRhlz56mMn/RSU7vQcWTobZYiJW8QccGKxBVdc\r\nuNhlwMROQxH5ksnl4fNDxNCST40nxXQ7auLyl57BNqtRq+fnLpMgswo4JfqUySnq\r\nTHxWG2FuouuZKFM3dZwEUmUCAwEAAQ==\r\n-----END RSA PUBLIC KEY-----";
        public const string PrivateKeyPemRsa = "-----BEGIN RSA PRIVATE KEY-----\r\nMIIJQQIBADANBgkqhkiG9w0BAQEFAASCCSswggknAgEAAoICAQCz229/wpoZF+Gw\r\nC6AXqfSGNHxofl/TyJ1Lqj/0v8UEgd7ZpwTIFk/L+Lt3tnhhbH91Jbt8q2JqE4Qz\r\n9u3RSJ+t0MIQTv4/9JTWnk5EnTexesxORNcEBt4/3DxYyipqMfrPb7c8O4bHKyg5\r\nWPlnI7pKJZ6QmaRdRftjx2dYWM/k597ymjS9GaPpS/dgC8do7Doz5wND6XFQClm5\r\ngB7V5FD3oFRExmojK2kNKi5ioGU8oatixe1XT68ATzQj9Io0rke1GK/Seaiwenms\r\nQyWCjTiNRwMtdxnLjYy/HuREt4FtlQ1xK+cmcGSiEo+VDlMIFbIQPEgC7eSkIW4z\r\nAGVkEMANUvKSIHYLQ+gRu8/lf6C14cfc2OFjqb1Tick7L89Vukvy3blAdU9GAtVX\r\nckIS0jnzAuJQon0neXgc6YNPXNydHG+prE3Ri/HJ/CtnobCJBRBZAYjejhj4uQwY\r\nHHKQTXFTM6Z6JNihMTRVuW8kZta8V6eWgUQZ0keYSMWjTdgOnn/wXXqyPmT1nQ4o\r\nzIc+PcoOgq5ICBGJnpkOwMUGKf5pzpXRY1GGXPnqYyf9FJTu9BxZOhtliIlbxBxw\r\nYrEFV1y42GXAxE5DEfmSyeXh80PE0JJPjSfFdDtq4vKXnsE2q1Gr5+cukyCzCjgl\r\n+pTJKepMfFYbYW6i65koUzd1nARSZQIDAQABAoICABrbN2kCa/Q3RrH86mjeLe8k\r\nQzdvN2vuVt6Hi4lGYWrs4ZPqhqJCAqRYfdXAX3VcuCOMANT62nUweNsxkg1gJMfV\r\nlkTNJtXx9Y+ej91bBIfx6DP/v4OQavtqLXCsr2ywDd2PtvK9iMCQxy7ZBFTMVvLf\r\nby/0YUC0RHd/vQTKLjDmFrpvIFTkUT9y4ntrBtm5/G7nnes0HoFvKjqy0OfrcdOo\r\nEy0523to7gTTOZ3siXFmSqszFt+kvGL1cLm1uDVpmLeH+ikZlYDUqPp6BE3cPIeM\r\nBQNy910Xw95+BGPmauMsEJfHkHqC6ePxlZMSUn+wjagy+CXH1A5WF5hyBLHOdn7G\r\nojaZCy+ihoh/fp4XojO41ARVKp8DhzF0xqfzKcg6/LpNq3ayqP/WGqCB/W0sbJrN\r\nI679WU22GhL1wCFInJUliK3GAdLVqNXUjJqdgIPbNh7X/h/mnfVOU7E60/gIkEkU\r\nsUhKHIKMwSzqmlJqokA0+kmQ/6JnRG588uGM8uBJXfxvFYzLVJSDR+CvxeT4Xfie\r\nEk71NQLzFgIcBMO1R7FH65Z9yl6X640z0/nGHVrKpTmR8htXJdY0qcazmlDS7qgk\r\nbRDsYYTmgqcGoLqn489GH78QCndH0V185dCa8ZQe+S6Yhy0mq8xDqxlRbO/HIKxS\r\nFjgbCackX6l3n+hbJAh5AoIBAQDkHORfJVBu0d7gMW1VO6ysBZb6PG1gE3p2Y1Ev\r\nFfhRF5/Z13xOLVA4IUpAbXZEXpSCP3wpPW33Y2kUMuSdtkntQWe8VmBLQzB4WIE6\r\nRq3H2f3Uo5jVKvSsbD9NEPOOVwGS5no7W/Ns7pq42UPud1j/1Dl6B7SdRADjsibV\r\nX3A7yDi0VsWB9ebE/TEWdPUKfU/6tCn2Z4M784QkIJFce3IZuZzLnPWF6BABb5oU\r\nXXxGvFEmZF+v22oxgPMYlGn4X7A6hA5PW+GGMFkjeG3PktEQqjxELJCS5QlcmAZY\r\no4CKN78KyLbYKU49TrVwW2ertOkdmL8BFzMTCTW4xXHp9LmjAoIBAQDJ2FCX4kPq\r\n2OfUrwNhsFc+c2WYxdBniFZvN1SijkT1ImWu0PZFlzIJAP9rH0smD3/xAYZwIzwq\r\nc3pWrA3OvpTXp414D3zGyTD/E9m9Zb1sj+0KIUkn9KmUyUuwvjINhYf2Y0N4+phI\r\nVZeJDhSpAy3JtmqZBPPBKBCH8AAce2lhJBOcUotzaXbBI9sZ/JstrUzxsNfQ/3V1\r\nFyYAIaoQIVMRE7Sv9YlnNOC6hzd38lzBXF0Q2IatRpOyu2vHVp5Er2pfsbysckTf\r\nxVA3/Lk8yt9LXTAeZ8meFvza3kTeDCgRKXm3Ptyorpe4rkrojL8yH74FJ4MqJQxi\r\nt6mVcGSdzZRXAoIBAB29JojA9Jt+APB5gSd7gCdtEyHMfvXnlC+bAxqAYr8vtUdR\r\nMIOApNSsgmGj46dGLVZNsIIv4AZAj5JykBt/iGPGAyAqoqMtP0RxRWezjzRe1xjB\r\nu0sPLpbMBfSmIRqNfUcJhSX39uRctw8iRBjytkekA1pFZuaZ1wPYVfe7WYZxs/LO\r\n3TD0PgGwgJXM6aVUcPjeRBo7pIBMXw3WsRy9e0KfUDG7ZMbwWiXVxuCgz3LpWisH\r\nKvJiSJXrmcW6k5Yt42u9i6AM+nnkE7rsGGTXXyotB25b4P1FntLTfwBPUJK0cdOW\r\nF0BXIjwb5sufHwTjsQCKyVv7Ck2RBc/w7cjyfsECggEAbvuc3NaRp5jaaAxPHLC8\r\nV99Vlpn2DGdft0lCJRt6+RqPtH67WupWnbLJvln3lRbir166UABfvau/MdbqxKnv\r\n51/+nmxNnHPVl/uPNt+xNvcwS+ifc/PIJsJD9wutM9gfOS+9UtDfWK6PYtG2NHRG\r\nAepmVHrvmF5yMHybYw3MlJgknEia2ru7i1kuyOwnWwc2DpLBJ+6+48H1qA7aloHs\r\nTbB7ImEsyu29P3LH4hEGRV+G4JNeAfqTjawWCAYtgfC92Z498sfl7PBuOkqgg7e8\r\n7ZBwu9cvediD8chTL5CZm04l2ccbxzgwvmA/WfzByErBzc27BjpWxBTejwe8mzQF\r\ngwKCAQAGnP0YdsHSqEMS5yxXkliGZkQgwUiTDBOwbo6lz4oqUjimYrSU2u6Y+dnt\r\nfMaQpHyFAUETHOVao+AIjHEngOyxLtXYmiYvMf0PNKA3IFhaI4KyKkIaFUF5LSpi\r\nlagb1YERtyY7/eU2Pt9AAjRdPaaVKneiNSVEr+gUkTraLyS3DNhEr2tu8nDCYoC/\r\niV0pcThWhhGaUuSLm3RXQP6P7JXLovEiVuLyeQNA2ZrIPOCJ639Ng9e7X7t6xLDw\r\na7v095Jz3BHWSSimY1PepTqllto+ZZ9RQIhJTIlF0d4vX3Mv/zpbwbCIrxNJQonq\r\n03wz5HlbyAbnbw7IZSBC59GEc3ec\r\n-----END RSA PRIVATE KEY-----";
        public const string PublicKeyXml = "<RSAKeyValue>\r\n  <Modulus>w+IbWHZONPaMUdxCxxZzBv9s92QxPimzGn97pTgCjhKmQB2f1CzKO2hBKSUgaanm4VXHoVtsKMi3TQKDzoomYlycnrhuHbxiQMhDvG/X3Sk29AtoCbdVI3O3uCx4snHX1zRM2wzTgYeeg7QFzXqhMZfqVVk11wyO2EbHoRXcAEfvs+TYjAqIAeqRhlfKwjr52iPftqyTGb7gMJTcE/TE8ZlehxSEFu/g3Tn6+eqnPUMMwl3Sc3NMtBvIm/3qEekP9AOpy1wzI97uwkcDs9M2otN4fm65Kyux/zzOMRC7Qks3ip/WYBRVlV1lm+seTL0tXg/lzIMmCe1ZaXd3OdHtBVUd6NYjD2mPhOQ5Es+vedoXtVb2/5GYCLCDL5KGzYQO1kkpRBk/fKh2Z6VBLga2G+jyge9GJB7+pVXb3zxsOOEPf+vR6ioD7kypeegY1D/BhC4LVtPtDeTb/c9dwth8EONQMxNkSBMLVUpW95OFAZnCV4/GNjTIFJuD/xtorplb/h75xZf/B6uT8kMbJKEz2bHWmm2brAGr77S8SADiT7DtOU10qEt1sIoLAdR9hUwZpaAbeUi1Gkc2Tw8DlpdHmR+WkVpLFWQVJpWP/lWcfLjTFVXQqFp6s9YwV+pIMrCym221mYMupQkIvhFA6ICITWVxO2jnbUfxt4lwt9DAZEU=</Modulus>\r\n  <Exponent>AQAB</Exponent>\r\n</RSAKeyValue>";
        public const string PrivateKeyXml = "<RSAKeyValue>\r\n  <Modulus>w+IbWHZONPaMUdxCxxZzBv9s92QxPimzGn97pTgCjhKmQB2f1CzKO2hBKSUgaanm4VXHoVtsKMi3TQKDzoomYlycnrhuHbxiQMhDvG/X3Sk29AtoCbdVI3O3uCx4snHX1zRM2wzTgYeeg7QFzXqhMZfqVVk11wyO2EbHoRXcAEfvs+TYjAqIAeqRhlfKwjr52iPftqyTGb7gMJTcE/TE8ZlehxSEFu/g3Tn6+eqnPUMMwl3Sc3NMtBvIm/3qEekP9AOpy1wzI97uwkcDs9M2otN4fm65Kyux/zzOMRC7Qks3ip/WYBRVlV1lm+seTL0tXg/lzIMmCe1ZaXd3OdHtBVUd6NYjD2mPhOQ5Es+vedoXtVb2/5GYCLCDL5KGzYQO1kkpRBk/fKh2Z6VBLga2G+jyge9GJB7+pVXb3zxsOOEPf+vR6ioD7kypeegY1D/BhC4LVtPtDeTb/c9dwth8EONQMxNkSBMLVUpW95OFAZnCV4/GNjTIFJuD/xtorplb/h75xZf/B6uT8kMbJKEz2bHWmm2brAGr77S8SADiT7DtOU10qEt1sIoLAdR9hUwZpaAbeUi1Gkc2Tw8DlpdHmR+WkVpLFWQVJpWP/lWcfLjTFVXQqFp6s9YwV+pIMrCym221mYMupQkIvhFA6ICITWVxO2jnbUfxt4lwt9DAZEU=</Modulus>\r\n  <Exponent>AQAB</Exponent>\r\n  <P>5NxCcnDZ7Q3KmhYRfG4e9TngCWvHxMsHYGzqht+FUTMvdvLSX5nIc1icydGgNbZ41/W33ig/Vcz5NGejLyTQbgdtpnsOVfLH7+JaDTjH7MTz0QXb7q+Xfzx+ASd3X8+CFapY+hFYPpf18rwhwN0wXcuVCUGI/phco676lKhyRZJZIoCre7xPzvWlORjpiDCm2Tubq1xGn3Pzzb6jV4wt7DZ0iRR2DTFCnHCE/0kxYn5z21GJD8+49f+R8AXkiIzgd69Tu0p/AluZqilgfAFQ2EhEnUI6fHO54tTYOT6g7H5gjbOCNcMINAnFc3klA3a1CnxLQxLZX6S+xFk0B67krw==</P>\r\n  <Q>2xy6JTS5nvzEGLqocq5jUyEwI0/TNAeyp8etUzPU7nLTUuK9d2moqySei4pzpEvbQpHDHhZB20rIAT86524s9FagFXckC6yIXQm7swuymwVtca4UH3WnQoRledIDYV75Akr5DMGFLvIbphN9QnwsXLuY1bXy5x15NOn21SDDL4n9+ktXdy+WGSrFOHEdNmUlejp4jRjoHc2iLPdcSHmE54XtYNbcA1STLWdFRpJghQpKJvcI9AM86HeFqo1zCupzZ7+s0or0cVypXZw8x4cLtoJ9H2sbXZ+EqYFBVqyEs5woro5PZlDql+rLZJDDLlyjhTI5sAVHcR1dvi4kbAYrSw==</Q>\r\n  <DP>IPKg0k7y4NDaeSJyNAK6jD1fxptwsCE2l6g2f2JQlCcVTz+YD9FY7Vo1gEygjvsNHLIwXVhX/ec7fVLqSFA5fY2uOIiAwNjp9dwVcM/a4HheTZpVVmCLI/M2hL4EzpNWO/5BPwceOCyyaNay2Uw+uVIky6dWrKiMtbfsNVim0uNF19TEW1T5/Gaa+cRfYo72hlGxkJMBS9VqpRUr+N+igN46Gr4KXM3ZtCHy07na1T41Ob7cR511GF1Jyk9e3lXBvxlLqawI6vL7BWddj5y04d5NmoI9X0td2I0h7+PXURTEm8HbHhhM5wj9Fym6rWv6ll7civR8vOs/146sEesY2w==</DP>\r\n  <DQ>NFwoC24PEXQbtEEesdEEgUAEDdwsf1l7kuZ/f8t6DcU4xKzUU+3W1Zb5oCTkv5U0/zJv6weskyfKpamjfNxyN61fseF3pqaDK6CAzydzdeIVJj5QJrhp0NZsnXDXR/R3Etoxq5+vIOnQO56ap0GfJEXcHu3M7ve3GDL0vjeHDRwvqhlI/H1NpBP5byRws6f2DcBdQHJ8PWr/Xzl5PwRzAxSZBjQx/UW/qfDsuBGQqRotYCRWamE/s9mkAn6syCYAkAFY3jNAeLqI6orEZ3XZEbMbDV/9+SB+hjcSExFE4NrnSddOGAgsF0OPeSeyzKhslw7KhEquqNQ4q5LBN2M+Aw==</DQ>\r\n  <InverseQ>pINHclKwKCqJ5XTZKleZBD92W1DhgeQ6uJPSS+p7acoRkTcbHmRz6nqm4SmXSZOUQROZ7hDbGrMRQsitf9g7Yn9BBars6H+MAvXJFMfwfWgAZUoxwZVOKhCOBJ+UwIvrKt6iBBdO6SuZqGuoBp4XstUdq8nd86dL6L+laHFjMdob09Gka5z+GwzGVJbG69KI9mqtAVUBgu+Py73Va45Qm/cRaeG1OQi4et+IMMHjLgeS+ODDqDSdZDUG0lTDdd0NV6jPDn/1Vq4pJwZOGSlTllTCUeQLFj8IHSkyxN0veL70rE5qhnRSsk6PLxoEjc42DEIjVed3krDl2f3UTNSbHw==</InverseQ>\r\n  <D>SO3V4ImrdGyWsaOf+R+Sk/hlSXY0H0DHPgZYspq32M14uEZPS1+hq5yVJgx8lNp860hUmu6+xjB0jmdxS2SEBfsVFML1vHx22Ee3cGDzrZO6sgHNKlDnkdtze06TYVyhT65ZLvUHiiJEEiTkRCxz91LoG/6q3PX/+OXhTUgmMLSXuhJpzTneiVgb9jiZdNcgTucsU4bM/bjjHzNM/OEUsh634vLwetm0jqkXmBSVT2m3RmZIBFPZKvVtKybODv0Gw4LZpK0fHSvTGOH3ruvkW5sAMZJnvnr2OmEZI4tnLLd0Zlg8KGlps8KiIfrXvRpx2aNxm0L41plXwLyJ1y0hYUQWCx74+TTwW/jpMK3sCeBoq9WdZ0a5rx9ao4I4+aFupc+yClreoC0uaCEW2zfpd2qSznONVEveuvCCy3qb9lqlFi5czL//NhQRH0j09rJXgNi+IbVhPu2DGbpx26VRbYPYwhE70sjr55ghHAgRN8Ek9dCuJw9pa5SGjwKirSehyzw0bVOoW9m5Gcgcj1PgJHoO1ejT+6+NKPrpe+kUOhLgLFESwNgQlREaAIVKJqQ6jwFjlFdBGu0kxDDF2bBByoaoNHDrGu+ypcYUoy9n7tG3Z+iMDhtgaXqllrFuE38qHxvqT3LrrZCwAmZa4/JpzP4JEIWsOHvnbYCVQrwRi8E=</D>\r\n</RSAKeyValue>";

        private void CreateKeyPair(uint size, RsaKeyPairEncoding encoding)
        {
            var keyPair = size.CreateRsaKeyPair(encoding);
            Console.WriteLine($"PublicKey: {Environment.NewLine}{keyPair.PublicKey}");
            Console.WriteLine($"PrivateKey: {Environment.NewLine}{keyPair.PrivateKey}");
            Assert.IsNotNull(keyPair);
        }

        [TestMethod]
        public void TestCreateKeyPairBerEncoded()
        {
            CreateKeyPair(KeySize, RsaKeyPairEncoding.Ber);
        }

        [TestMethod]
        public void TestCreateKeyPairPemEncoded()
        {
            CreateKeyPair(KeySize, RsaKeyPairEncoding.Pem);
        }

        [TestMethod]
        public void TestCreateKeyPairXmlEncoded()
        {
            CreateKeyPair(KeySize, RsaKeyPairEncoding.Xml);
        }


        [TestMethod]
        public void TestCreateKeyPairPemRsaEncoded()
        {
            CreateKeyPair(KeySize, RsaKeyPairEncoding.PemRsa);
        }


        private bool ValidateRsaKey(string publicKey, string privateKey)
        {
            Console.WriteLine($"PublicKey: {Environment.NewLine}{publicKey}");
            var result = publicKey.ValidateRsaPublicKey();
            Console.WriteLine($"Validated!{Environment.NewLine}");
            Console.WriteLine($"PrivateKey: {Environment.NewLine}{privateKey}");
            privateKey.ValidateRsaPrivateKey();
            Console.WriteLine($"Validated!");
            return result;
        }

        private bool ValidateRsaPublicKey(string publicKey)
        {
            Console.WriteLine($"PublicKey: {Environment.NewLine}{publicKey}");
            var result = publicKey.ValidateRsaPublicKey();
            Console.WriteLine($"Validated!");
            return result;
        }

        private bool ValidateRsaPrivateKey(string privateKey)
        {
            Console.WriteLine($"PrivateKey: {Environment.NewLine}{privateKey}");
            var result  = privateKey.ValidateRsaPrivateKey();
            Console.WriteLine($"Validated!");
            return result;
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestValidateNullRsaPublicKey()
        {
            string publicKey = null!;
            Assert.IsFalse(ValidateRsaPublicKey(publicKey));
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestValidateNullRsaPrivateKey()
        {
            string privateKey = null!;
            Assert.IsFalse(ValidateRsaPrivateKey(privateKey));
        }

        [TestMethod]
        public void TestValidateRsaKeysBerEncoded()
        {

            ValidateRsaKey(PublicKeyBer, PrivateKeyBer);
        }

        [TestMethod]
        public void TestValidateRsaKeysPemEncoded()
        {

            ValidateRsaKey(PublicKeyPem, PrivateKeyPem);
        }

        [TestMethod]
        public void TestValidateRsaKeysXmlEncodedIndented()
        {
            ValidateRsaKey(PublicKeyXml, PrivateKeyXml);
        }

        [TestMethod]
        public void TestValidateRsaKeysPemRsaEncoded()
        {

            ValidateRsaKey(PublicKeyPemRsa, PrivateKeyPemRsa);
        }

        public void EncryptDecryptRsa(string data, string publicKey, string privateKey, IEncoder encoder)
        {
            var encrypted = data.EncryptEncodedRsa(publicKey, encoder);
            string decrypted = encrypted.DecryptEncodedRsa(privateKey, encoder).ToStringUtf8();
            Console.WriteLine($"PublicKey: {Environment.NewLine}{publicKey}");
            Console.WriteLine($"PrivateKey: {Environment.NewLine}{privateKey}");
            Console.WriteLine();
            Console.WriteLine($"Data: {data}");
            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");
            Assert.AreEqual(data, decrypted);
        }


        [TestMethod]
        public void TestRsaEncryptBerEncodedKeyBase64Encoder()
        {
            EncryptDecryptRsa(Data, PublicKeyBer, PrivateKeyBer, Base64Encoder.DefaultInstance);
        }

        [TestMethod]
        public void TestRsaEncryptPemEncodedKeyBase64Encoder()
        {
            EncryptDecryptRsa(Data, PublicKeyPem, PrivateKeyPem, Base64Encoder.DefaultInstance);
        }

        [TestMethod]
        public void TestRsaEncryptPemRsaEncodedKeyBase64Encoder()
        {
            EncryptDecryptRsa(Data, PublicKeyPemRsa, PrivateKeyPemRsa, Base64Encoder.DefaultInstance);
        }

        [TestMethod]
        public void TestRsaEncryptXmlEncodedKeyIndentedBase64Encoder()
        {
            EncryptDecryptRsa(Data, PublicKeyXml, PrivateKeyXml, Base64Encoder.DefaultInstance);
        }


        [TestMethod]
        public void TestKey()
        {
            var publicKey = "-----BEGIN PUBLIC KEY-----\rMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAyIxMUan2IHO2b8k2bnb9\r\nC1fijYnd1I12qLi9BthyocXpcfNVqXO0AQvO79xX/YcMfPoaom65znxz1Xt50Ch1\r\n1iXoy/p4ui6dfQat6ig3s5NsrCB2MmOJiYcD7nCtUqdlb26KlPS48a1yrw3URYcB\r\nqMog9YAuX6yL/S1oQiBRkera2Tmtlm7iajCGpQdM8tGx49s2A7fUUaZBZr7Q+K63\r\n9TrJcTT8E9CKx6F+GNgooDNgfCIW9PJAFifVOODUzWHwCzzut967OPez23K5pxGc\r\nhJh9Almvbd/CUXoToj19MLUbBpMVj3eMHKm8NqA21keQcU/XBE1E+oX0HvSFPZcv\r\nj/gXxhSGo2blb6d8VPFdIjBfcEsRM+cWLpAJ3FeUDKaKHPWrZI3N84TOX+ZJ0Fu2\r\nOMXdrG6UQQpwPd8WwV60AzAin2Y8Wy4DugbC2XciB3e7Pzqk+quiUKJOnWEexZxg\r\nnUW8xBVP9p1vH1W6knZ8Q0T4qsKrp9PRTWwe1bVt2QXaOu+GD8fKiacLTqyHugyL\r\nLhNRopLQ6JsD0VtscDwr4mh5pVSFl4wNzYlqJZcYhcEbWxEtD91a+5gCV4PWC4kK\r\nkj5Njs96OaznrsiIhiNNOGL5wRQqp11YUqH8XmiNgXD5e3q+XJWrr6hqv8uemcvu\r\nWmoCF5fLx7tA1EI0qSDzES0CAwEAAQ==\r\n-----END PUBLIC KEY-----";
            var privateKey = "-----BEGIN PRIVATE KEY-----\n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\r\npKV5jlcBRkCPdd1PHYhkWqiI3F7Fewk=\r\n-----END PRIVATE KEY-----";
            var encrypted = Data.EncryptEncodedRsa(publicKey, Base64Encoder.DefaultInstance);
            Assert.AreEqual(Data, encrypted.DecryptEncodedRsa(privateKey, Base64Encoder.DefaultInstance).ToStringUtf8());
        }
    }
}
