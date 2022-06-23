using Microsoft.Maui.Controls.Shapes;
using Xunit;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	public class PathFigureCollectionTests
	{
		PathFigureCollectionConverter _pathFigureCollectionConverter;

		
		public void SetUp()
		{
			_pathFigureCollectionConverter = new PathFigureCollectionConverter();
		}

		[Fact]
		public void ConvertStringToPathFigureCollectionTest()
		{
			PathFigureCollection result = _pathFigureCollectionConverter.ConvertFromInvariantString("M 10,100 C 100,0 200,200 300,100") as PathFigureCollection;

			Assert.NotNull(result);
			Assert.Equal(1, result.Count);
		}

		[Fact]
		[TestCase("M8.4580019,25.5C8.4580019,26.747002 10.050002,27.758995 12.013003,27.758995 13.977001,27.758995 15.569004,26.747002 15.569004,25.5z M19.000005,10C16.861005,9.9469986 14.527004,12.903999 14.822002,22.133995 14.822002,22.133995 26.036002,15.072998 20.689,10.681999 20.183003,10.265999 19.599004,10.014999 19.000005,10z M4.2539991,10C3.6549998,10.014999 3.0710002,10.265999 2.5649996,10.681999 -2.7820019,15.072998 8.4320009,22.133995 8.4320009,22.133995 8.7270001,12.903999 6.3929995,9.9469986 4.2539991,10z M11.643,0C18.073003,0 23.286002,5.8619995 23.286002,13.091995 23.286002,20.321999 18.684003,32 12.254,32 5.8239992,32 1.8224728E-07,20.321999 0,13.091995 1.8224728E-07,5.8619995 5.2129987,0 11.643,0z", TestName = "AlienPathTest")]
		[TestCase("M16.484421,0.73799322C20.831404,0.7379931 24.353395,1.1259904 24.353395,1.6049905 24.353395,2.0839829 20.831404,2.4719803 16.484421,2.47198 12.138443,2.4719803 8.6154527,2.0839829 8.6154527,1.6049905 8.6154527,1.1259904 12.138443,0.7379931 16.484421,0.73799322z M1.9454784,0.061995983C2.7564723,5.2449602 12.246436,11.341911 12.246436,11.341911 13.248431,19.240842 9.6454477,17.915854 9.6454477,17.915854 7.9604563,18.897849 6.5314603,17.171859 6.5314603,17.171859 4.1084647,18.29585 3.279473,15.359877 3.2794733,15.359877 0.82348057,15.291876 1.2804796,11.362907 1.2804799,11.362907 -1.573514,10.239915 1.2344746,6.3909473 1.2344746,6.3909473 -1.3255138,4.9869594 1.9454782,0.061996057 1.9454784,0.061995983z M30.054371,0C30.054371,9.8700468E-08 33.325355,4.9249634 30.765367,6.3289513 30.765367,6.3289513 33.574364,10.177919 30.71837,11.30191 30.71837,11.30191 31.175369,15.22988 28.721384,15.297872 28.721384,15.297872 27.892376,18.232854 25.468389,17.110862 25.468389,17.110862 24.040392,18.835847 22.355402,17.853852 22.355402,17.853852 18.752417,19.178845 19.753414,11.279907 19.753414,11.279907 29.243385,5.1829566 30.054371,0z", TestName = "AngelPathTest")]
		[TestCase("M16.000002,21.077007L20.708025,23.275002 19.86202,25.086996 16.000002,23.284 12.136983,25.086996 11.291979,23.275002z M19.94001,15.458007C21.018013,15.458007 21.892015,16.312004 21.892015,17.366001 21.892015,18.419998 21.018013,19.273996 19.94001,19.273996 18.862009,19.273996 17.988007,18.419998 17.988007,17.366001 17.988007,16.312004 18.862009,15.458007 19.94001,15.458007z M12.059019,15.458007C13.137022,15.458007 14.011024,16.312004 14.011024,17.366001 14.011024,18.419998 13.137022,19.273996 12.059019,19.273996 10.981017,19.273996 10.107016,18.419998 10.107016,17.366001 10.107016,16.312004 10.981017,15.458007 12.059019,15.458007z M7.827992,9.5650053L16.000006,14.811005 24.172022,9.5650053 25.252024,11.249004 16.000006,17.188004 6.7479901,11.249004z M16,2C8.2799683,2 2,8.2799988 2,16 2,23.720001 8.2799683,30 16,30 23.719971,30 30,23.720001 30,16 30,8.2799988 23.719971,2 16,2z M16,0C24.82196,0 32,7.1779938 32,16 32,24.821991 24.82196,32 16,32 7.1779785,32 0,24.821991 0,16 0,7.1779938 7.1779785,0 16,0z", TestName = "AngryPathTest")]
		[TestCase("M13.952596,15.068143C13.767538,15.066144 13.583578,15.095151 13.403586,15.157148 12.252587,15.553147 11.725549,17.163162 12.224572,18.753189 12.725547,20.342192 14.062582,21.309212 15.211566,20.914204 16.362564,20.518204 16.889541,18.908188 16.390579,17.318163 15.968584,15.977162 14.95058,15.077146 13.952596,15.068143z M7.7945876,6.1100698C7.2026091,6.0760732 6.4365583,6.7850791 5.9736071,7.8550807 5.4445558,9.0761004 5.5105953,10.302109 6.1215563,10.590106 6.7316013,10.881108 7.65555,10.126112 8.1855779,8.9070922 8.7145686,7.6860881 8.6485896,6.4610711 8.036592,6.1710754 7.9606028,6.1350642 7.8795486,6.1150752 7.7945876,6.1100698z M15.404559,5.9590679C15.383563,5.9580608 15.362566,5.9580608 15.34157,5.960075 14.674579,6.0020671 14.194539,7.1220723 14.275593,8.4590903 14.354573,9.7981063 14.962543,10.848119 15.631547,10.802114 16.300554,10.759113 16.778579,9.6401005 16.700576,8.3020907 16.622573,7.006074 16.049577,5.980064 15.404559,5.9590679z M12.317589,1.4699259E-05C15.527545,0.0050196948 18.757579,1.2870288 21.236579,3.8010436 24.038576,6.6430793 25.533567,12.005127 25.825559,15.861164 26.09155,19.371191 27.844537,19.518194 30.765552,22.228211 31.592515,22.995216 33.904521,25.825243 28.733512,26.053242 26.619564,26.146244 25.60156,25.739243 21.732549,22.850226 21.235542,22.545214 20.664558,22.733219 20.373542,22.885214 20.017526,23.07122 19.741586,23.925232 19.851572,24.215227 20.16456,25.583237 22.25855,25.135235 23.427553,26.313253 24.41156,27.305252 22.795536,29.807287 18.926586,29.29027 18.926586,29.29027 16.343582,28.587277 13.853597,25.258236 11.910547,25.242245 9.6305823,25.258236 9.6305823,25.258236 9.6305823,25.258236 9.6025672,26.705256 9.6425452,27.10626 10.271573,27.256254 10.777553,27.021252 13.298544,27.736271 14.150593,27.978262 16.663589,31.170292 8.7236018,30.424282 7.0135832,30.263287 7.1875944,30.721283 5.2576051,26.025242 4.2626119,23.604229 2.0076115,22.396212 0.6345674,17.082169 -0.27241354,14.207143 -0.21040192,11.068107 0.84159805,8.2280856 0.97556992,7.8450862 1.1235799,7.5130826 1.2786091,7.1980773 1.8406196,6.0020671 2.5815849,4.8720523 3.5156043,3.863056 5.9166007,1.2680314 9.107573,-0.0049901602 12.317589,1.4699259E-05z", TestName = "GhostPathTest")]
		[TestCase("M27.915988,16.084991C24.186011,18.076004 19.902995,19.220001 15.330003,19.220001 11.187978,19.220001 7.2869802,18.263 3.8149987,16.608994 4.2960163,22.921997 9.4639798,27.914001 15.840012,27.914001 22.389993,27.914001 27.694005,22.651001 27.915988,16.084991z M20.936015,7.0749969C19.574994,7.0749969 18.467023,8.3109894 18.467023,9.8430023 18.467023,11.371002 19.574994,12.612 20.936015,12.612 22.299966,12.612 23.405984,11.371002 23.405984,9.8430023 23.405984,8.3109894 22.299966,7.0749969 20.936015,7.0749969z M10.57903,7.0749969C9.2150183,7.0749969 8.1099778,8.3109894 8.1099778,9.8430023 8.1099778,11.371002 9.2150183,12.612 10.57903,12.612 11.941029,12.612 13.048022,11.371002 13.048022,9.8430023 13.048022,8.3109894 11.941029,7.0749969 10.57903,7.0749969z M15.840988,0C24.587989,0 31.681001,7.1649933 31.681001,16 31.681001,24.835999 24.587989,32 15.840988,32 7.0920344,32 -5.1397365E-08,24.835999 0,16 -5.1397365E-08,7.1649933 7.0920344,0 15.840988,0z", TestName = "HappyPathTest")]
		[TestCase("M7.4559937,18.497009C7.4559937,18.497009 10.204987,19.944 16,19.944 21.794983,19.944 24.543976,18.497009 24.543976,18.497009 24.543976,22.527008 21.667999,27.041992 16,27.041992 10.332001,27.041992 7.4559937,22.527008 7.4559937,18.497009z M20.994995,11.005005C22.372986,11.005005 24.741974,12.721985 24.741974,13.502014 24.741974,14.28299 22.372986,13.502014 20.994995,13.502014 19.616974,13.502014 18.497986,14.380005 18.497986,13.502014 18.497986,12.623993 19.616974,11.005005 20.994995,11.005005z M11.004974,11.005005C12.382996,11.005005 13.502991,12.623993 13.502991,13.502014 13.502991,14.380005 12.382996,13.502014 11.004974,13.502014 9.6269836,13.502014 7.2589722,14.28299 7.2589722,13.502014 7.2589722,12.721985 9.6269836,11.005005 11.004974,11.005005z M16,2.4970093C8.5549927,2.4970093 2.4979858,8.5549927 2.4979858,16 2.4979858,23.445007 8.5549927,29.502991 16,29.502991 23.444977,29.502991 29.502991,23.445007 29.502991,16 29.502991,8.5549927 23.444977,2.4970093 16,2.4970093z M16,0C24.836975,0 32,7.1629944 32,16 32,24.837006 24.836975,32 16,32 7.1629944,32 0,24.837006 0,16 0,7.1629944 7.1629944,0 16,0z", TestName = "LaughPathTest")]
		[TestCase("M8.52891,4.1079743C7.8829048,4.1079743 7.2528914,4.2500036 6.6538837,4.531986 5.5498646,5.0479777 4.71184,5.9749806 4.286846,7.1420043 3.8638666,8.3079903 3.9078734,9.5739825 4.4118479,10.704995 5.4078944,12.945995 11.830959,20.130998 15.987023,24.54302 21.348061,18.870012 26.721182,12.65501 27.5882,10.704995 28.092173,9.5739825 28.137158,8.3079903 27.712164,7.1420043 27.289183,5.9749806 26.449145,5.0479777 25.345125,4.531986 24.748133,4.2500036 24.117142,4.1079743 23.470099,4.1079743 21.820113,4.1079743 20.734099,5.0509989 19.500073,6.5119984L16.001,10.18998 12.499975,6.5119984C11.245928,5.038975,10.180911,4.1079743,8.52891,4.1079743z M8.5309241,0C11.559959,-1.7678713E-07 14.463015,1.6680005 16.001,4.5209997 18.138054,0.55099513 22.921141,-1.1230175 27.007197,0.79498353 31.297236,2.8069785 33.186238,8.0069955 31.227228,12.411997 29.274197,16.804 15.998009,30.38 15.998009,30.38 15.917014,30.327022 2.7268267,16.804 0.77379643,12.411997 -1.1852157,8.0069955 0.70378798,2.8069785 4.9918731,0.79498353 6.1408761,0.25497441 7.3459104,-1.7678713E-07 8.5309241,0z", TestName = "LovePathTest")]
		[TestCase("M16,19.85202C19.743988,19.85202 23.047974,21.71701 25.048981,24.563019 22.498993,22.729004 19.380981,21.637024 16,21.637024 12.618988,21.637024 9.5009766,22.729004 6.9509888,24.563019 8.9519958,21.71701 12.255981,19.85202 16,19.85202z M20.994995,11.005005C22.372986,11.005005 23.492981,12.123016 23.492981,13.503021 23.492981,14.882019 22.372986,16 20.994995,16 19.616974,16 18.497986,14.882019 18.497986,13.503021 18.497986,12.123016 19.616974,11.005005 20.994995,11.005005z M11.004974,11.005005C12.382996,11.005005 13.502991,12.123016 13.502991,13.503021 13.502991,14.882019 12.382996,16 11.004974,16 9.6269836,16 8.5069885,14.882019 8.5069885,13.503021 8.5069885,12.123016 9.6269836,11.005005 11.004974,11.005005z M16,2.4970093C8.5549927,2.4970093 2.4979858,8.5550232 2.4979858,16 2.4979858,23.445007 8.5549927,29.503021 16,29.503021 23.444977,29.503021 29.502991,23.445007 29.502991,16 29.502991,8.5550232 23.444977,2.4970093 16,2.4970093z M16,0C24.836975,0 32,7.1629944 32,16 32,24.837006 24.836975,32 16,32 7.1629944,32 0,24.837006 0,16 0,7.1629944 7.1629944,0 16,0z", TestName = "SadPathTest")]
		public void ComplexPathTest(string path)
		{
			PathFigureCollection result = _pathFigureCollectionConverter.ConvertFromInvariantString(path) as PathFigureCollection;

			Assert.NotNull(result);
			Assert.AreNotEqual(0, result.Count);
		}
	}
}